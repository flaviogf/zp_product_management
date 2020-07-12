using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ZPProductManagement.Application;
using ZPProductManagement.Application.Categories;
using ZPProductManagement.Common;

namespace ZPProductManagement.Web.Infrastructure
{
    public class DapperCategoryRepository : ICategoryRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DapperCategoryRepository> _logger;

        public DapperCategoryRepository(IUnitOfWork uow, ILogger<DapperCategoryRepository> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Maybe<ICategoryAdapter>> FindByName(string name)
        {
            try
            {
                var sql = @"
                    SELECT TOP 1 [Id], [Name] FROM
                    [dbo].[Categories]
                    WHERE [Name] = @Name
                ";

                var param = new
                {
                    Name = name
                };

                var categoryAdapter = await _uow.Connection.QueryFirstOrDefaultAsync<InputCategoryAdapter>(sql, param, transaction: _uow.Transaction);

                return categoryAdapter;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return null;
            }
        }
    }
}