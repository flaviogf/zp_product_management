﻿using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPProductManagement.Application;
using ZPProductManagement.Application.Products;
using ZPProductManagement.Common;

namespace ZPProductManagement.Web.Infrastructure
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DapperProductRepository> _logger;

        public DapperProductRepository(IUnitOfWork uow, ILogger<DapperProductRepository> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Result> Save(IProductAdapter productAdapter)
        {
            try
            {
                var insertProduct = "INSERT INTO [dbo].[Products] ([Id], [CategoryId], [Name], [Description], [Price], [Quantity]) VALUES (@Id, @CategoryId, @Name, @Description, @Price, @Quantity)";

                await _uow.Connection.ExecuteAsync(insertProduct, productAdapter, transaction: _uow.Transaction);

                var insertFile = "INSERT INTO [dbo].[ProductFiles] ([ProductId], [FileId]) VALUES (@ProductId, @FileId)";

                var tasks = productAdapter.FileIds.Select(it => _uow.Connection.ExecuteAsync(insertFile, new { ProductId = productAdapter.Id, FileId = it }, transaction: _uow.Transaction));

                await Task.WhenAll(tasks);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return Result.Fail("Oops... something has been wrong");
            }
        }

        public async Task<Maybe<IProductAdapter>> FindById(Guid id)
        {
            try
            {
                var selectProduct = "SELECT TOP 1 p.[Id], p.[Name], p.[Description], p.[Price], p.[Quantity], c.[Id] [CategoryId], c.[Name] [CategoryName] FROM [dbo].[Products] p JOIN [dbo].[Categories] c ON p.[CategoryId] = c.[Id] WHERE p.Id = @Id";

                var selectProductParam = new
                {
                    Id = id
                };

                var product = await _uow.Connection.QueryFirstOrDefaultAsync<IndexProductAdapter>(selectProduct, param: selectProductParam, transaction: _uow.Transaction);

                var selectFiles = "SELECT f.[Id], f.[Name], f.[Path], f.[Extension] FROM ProductFiles pf JOIN Files f ON pf.[FileId] = f.[Id] WHERE pf.[ProductId] = @ProductId";

                var selectFilesParam = new
                {
                    ProductId = product.Id
                };

                var files = await _uow.Connection.QueryAsync<InputFileAdapter>(selectFiles, param: selectFilesParam, transaction: _uow.Transaction);

                return new ShowProductAdapter(product.Id, product.Name, product.Description, product.Price, product.Quantity, product.CategoryId, product.CategoryName, files);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return null;
            }
        }

        public async Task<IEnumerable<IProductAdapter>> FindAll()
        {
            try
            {
                var selectProducts = "SELECT p.[Id], p.[Name], p.[Description], p.[Price], p.[Quantity], c.[Id] [CategoryId], c.[Name] [CategoryName] FROM [dbo].[Products] p JOIN [dbo].[Categories] c ON p.[CategoryId] = c.[Id]";

                var products = await _uow.Connection.QueryAsync<IndexProductAdapter>(selectProducts, transaction: _uow.Transaction);

                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return new List<IProductAdapter>();
            }
        }

        public async Task<Pagination<IProductAdapter>> Pagination(int page, int perPage)
        {
            try
            {
                var countProducts = "SELECT COUNT(*) FROM [dbo].[Products]";

                var total = await _uow.Connection.QueryFirstOrDefaultAsync<int>(countProducts, transaction: _uow.Transaction);

                var selectProducts = "SELECT p.[Id], p.[Name], p.[Description], p.[Price], p.[Quantity], c.[Id] [CategoryId], c.[Name] [CategoryName] FROM [dbo].[Products] p JOIN [dbo].[Categories] c ON p.[CategoryId] = c.[Id] ORDER BY p.[Name] OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

                var param = new
                {
                    Skip = (page - 1) * perPage,
                    Take = perPage
                };

                var content = await _uow.Connection.QueryAsync<IndexProductAdapter>(selectProducts, param: param, transaction: _uow.Transaction);

                var pages = total / (double)perPage;

                var pagination = new Pagination<IProductAdapter>(content, total, page, (int)Math.Ceiling(pages));

                return pagination;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return new Pagination<IProductAdapter>(new List<IProductAdapter>(), 0, 0, 0);
            }
        }
    }
}