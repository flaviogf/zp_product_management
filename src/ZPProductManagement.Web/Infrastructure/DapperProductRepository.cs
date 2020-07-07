using Dapper;
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

                var insertFiles = "INSERT INTO [dbo].[ProductFiles] ([ProductId], [FileId]) VALUES (@ProductId, @FileId)";

                var tasks = productAdapter.FileIds.Select(it => _uow.Connection.ExecuteAsync(insertFiles, new { ProductId = productAdapter.Id, FileId = it }, transaction: _uow.Transaction));

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
                var sql = "SELECT p.[Id], p.[Name], p.[Description], p.[Price], p.[Quantity], c.[Id] [CategoryId], c.[Name] [CategoryName] FROM [dbo].[Products] p JOIN [dbo].[Categories] c ON p.[CategoryId] = c.[Id] WHERE p.Id = @Id";

                var param = new
                {
                    Id = id
                };

                var product = await _uow.Connection.QueryFirstOrDefaultAsync<IndexProductAdapter>(sql, param, transaction: _uow.Transaction);

                return product;
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
                var sql = "SELECT p.[Id], p.[Name], p.[Description], p.[Price], p.[Quantity], c.[Id] [CategoryId], c.[Name] [CategoryName] FROM [dbo].[Products] p JOIN [dbo].[Categories] c ON p.[CategoryId] = c.[Id]";

                var products = await _uow.Connection.QueryAsync<IndexProductAdapter>(sql, transaction: _uow.Transaction);

                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return new List<IProductAdapter>();
            }
        }
    }
}