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

        public async Task<Result> Execute(CreateFile createFile)
        {
            var fileOrError = await GetFileOrError(createFile);

            if (fileOrError.Failure)
            {
                return Result.Fail(fileOrError.Message);
            }

            var createdFileOrError = await SaveFileOrError(fileOrError.Value);

            if (createdFileOrError.Failure)
            {
                return Result.Fail(createdFileOrError.Message);
            }

            return Result.Ok();
        }

        private async Task<Result<File>> GetFileOrError(CreateFile createFile)
        {
            var maybeStoredFile = await _fileRepository.FindByName(createFile.Name);

            if (maybeStoredFile.HasValue)
            {
                return Result.Fail<File>("Name is already taken");
            }

            var idOrError = Identifier.Of(createFile.Id);

            var nameOrError = Name.Of(createFile.Name);

            var pathOrError = Path.Of(createFile.Path);

            var extensionOrError = Extension.Of(createFile.Extension);

            var result = Result.Combine(nameOrError, pathOrError, extensionOrError);

            if (result.Failure)
            {
                return Result.Fail<File>(result.Message);
            }

            var file = new File(idOrError.Value, nameOrError.Value, pathOrError.Value, extensionOrError.Value);

            return Result.Ok(file);
        }

        private async Task<Result<CreatedFile>> SaveFileOrError(File file)
        {
            var createdFile = new CreatedFile(file);

            var result = await _fileRepository.Save(createdFile);

            if (result.Failure)
            {
                return Result.Fail<CreatedFile>(result.Message);
            }

            return Result.Ok(createdFile);
        }
    }
}