using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using ZPProductManagement.Application;
using ZPProductManagement.Common;

namespace ZPProductManagement.Api.Infrastructure
{
    public class LocalFileStorage : IFileStorage
    {
        private readonly ILogger<LocalFileStorage> _logger;
        private readonly IConfiguration _configuration;

        public LocalFileStorage(ILogger<LocalFileStorage> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Result> Save(CreatedFile createdFile)
        {
            try
            {
                var destination = _configuration["Upload:Destination"];

                var input = createdFile.Content;

                using var output = new FileStream(Path.Combine(destination, createdFile.Path), FileMode.Create, FileAccess.Write);

                await input.CopyToAsync(output);

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
