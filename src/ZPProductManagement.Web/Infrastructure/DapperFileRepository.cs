using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ZPProductManagement.Application;
using ZPProductManagement.Application.Files;
using ZPProductManagement.Common;

namespace ZPProductManagement.Web.Infrastructure
{
    public class DapperFileRepository : IFileRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DapperFileRepository> _logger;

        public DapperFileRepository(IUnitOfWork uow, ILogger<DapperFileRepository> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Result> Save(CreatedFile file)
        {
            try
            {
                var sql = "INSERT INTO [dbo].[Files] ([Id], [Name], [Path], [Extension]) VALUES (@Id, @Name, @Path, @Extension)";

                await _uow.Connection.ExecuteAsync(sql, file, transaction: _uow.Transaction);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return Result.Fail("Oops... something has gone wrong");
            }
        }

        public async Task<Maybe<StoredFile>> FindByName(string name)
        {
            try
            {
                var sql = "SELECT TOP 1 * FROM [dbo].[Files] WHERE [Name] = @Name";

                var param = new
                {
                    Name = name
                };

                var storedFile = await _uow.Connection.QueryFirstOrDefaultAsync<StoredFile>(sql, param, transaction: _uow.Transaction);

                return storedFile;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return null;
            }
        }
    }
}