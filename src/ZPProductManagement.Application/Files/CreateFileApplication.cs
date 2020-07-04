using System.Threading.Tasks;
using ZPProductManagement.Common;
using ZPProductManagement.Domain.Entities;
using ZPProductManagement.Domain.ValueObjects;

namespace ZPProductManagement.Application.Files
{
    public class CreateFileApplication
    {
        private readonly IFileRepository _fileRepository;

        public CreateFileApplication(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<Result> Execute(IFileAdapter fileAdapter)
        {
            var getFileOrError = await GetFileOrError(fileAdapter);

            if (getFileOrError.Failure)
            {
                return Result.Fail(getFileOrError.Message);
            }

            var saveFileOrError = await SaveFileOrError(getFileOrError.Value);

            if (saveFileOrError.Failure)
            {
                return Result.Fail(saveFileOrError.Message);
            }

            return Result.Ok();
        }

        private async Task<Result<File>> GetFileOrError(IFileAdapter fileAdapter)
        {
            var maybeFile = await _fileRepository.FindByName(fileAdapter.Name);

            if (maybeFile.HasValue)
            {
                return Result.Fail<File>("Name is already taken");
            }

            var idOrError = Identifier.Of(fileAdapter.Id);

            var nameOrError = Name.Of(fileAdapter.Name);

            var pathOrError = Path.Of(fileAdapter.Path);

            var extensionOrError = Extension.Of(fileAdapter.Extension);

            var result = Result.Combine(nameOrError, pathOrError, extensionOrError);

            if (result.Failure)
            {
                return Result.Fail<File>(result.Message);
            }

            var file = new File(idOrError.Value, nameOrError.Value, pathOrError.Value, extensionOrError.Value);

            return Result.Ok(file);
        }

        private async Task<Result<File>> SaveFileOrError(File file)
        {
            IFileAdapter fileAdapter = new OutputFileAdapter(file);

            var result = await _fileRepository.Save(fileAdapter);

            if (result.Failure)
            {
                return Result.Fail<File>(result.Message);
            }

            return Result.Ok(file);
        }
    }
}