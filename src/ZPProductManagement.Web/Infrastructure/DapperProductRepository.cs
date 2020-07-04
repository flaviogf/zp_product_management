using Dapper;
using Microsoft.Extensions.Logging;
using System;
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

        public async Task<Result> Save(CreatedProduct createdProduct)
        {
            try
            {
                var insertProductSql = "INSERT INTO [dbo].[Products] ([Id], [CategoryId], [Name], [Description], [Price], [Quantity]) VALUES (@Id, @CategoryId, @Name, @Description, @Price, @Quantity)";

                await _uow.Connection.ExecuteAsync(insertProductSql, createdProduct, transaction: _uow.Transaction);

                var insertProductFilesSql = "INSERT INTO [dbo].[ProductFiles] ([ProductId], [FileId]) VALUES (@ProductId, @FileId)";

                var tasks = createdProduct.Files.Select(it => _uow.Connection.ExecuteAsync(insertProductFilesSql, new { it.ProductId, it.FileId }, transaction: _uow.Transaction));

                await Task.WhenAll(tasks);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return Result.Fail("Oops... something has been wrong");
            }
        }

        public async Task<Maybe<StoredProduct>> FindById(Guid id)
        {
            try
            {
                var sql = "SELECT TOP 1 * FROM [dbo].[Products] WHERE [Id] = @Id";

                var param = new
                {
                    Id = id
                };

                var storedProduct = await _uow.Connection.QueryFirstOrDefaultAsync<StoredProduct>(sql, param, transaction: _uow.Transaction);

                return storedProduct;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return null;
            }
        }
    }
}