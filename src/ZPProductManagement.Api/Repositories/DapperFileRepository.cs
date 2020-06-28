using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ZPProductManagement.Application;
using ZPProductManagement.Common;

namespace ZPProductManagement.Api.Repositories
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

        public async Task<Maybe<StoredFile>> FindOne(Guid id)
        {
            try
            {
                var sql = @"
                    SELECT TOP 1 * FROM [dbo].[Files]
                    WHERE [Id] = @Id;
                ";

                var param = new
                {
                    Id = id
                };

                var storedFile = await _uow.Connection.QueryFirstOrDefaultAsync<StoredFile>(sql, param: param, transaction: _uow.Transaction);

                return storedFile;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return null;
            }
        }

        public async Task<Result> Save(CreatedFile createdFile)
        {
            try
            {
                var sql = @"
                    INSERT INTO [dbo].[Files]
                    (
                        [Id],
                        [Name],
                        [Path],
                        [Ext]
                    )
                    VALUES
                    (
                        @Id,
                        @Name,
                        @Path,
                        @Ext
                    );
                ";

                var param = new
                {
                    createdFile.Id,
                    createdFile.Name,
                    createdFile.Path,
                    createdFile.Ext
                };

                await _uow.Connection.ExecuteAsync(sql, param: param, transaction: _uow.Transaction);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return Result.Fail("Unable to save the file");
            }
        }
    }
}
