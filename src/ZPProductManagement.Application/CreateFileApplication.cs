using System.Threading.Tasks;
using ZPProductManagement.Common;
using ZPProductManagement.Domain;

namespace ZPProductManagement.Application
{
    public class CreateFileApplication
    {
        private readonly IFileRepository _fileRepository;
        private readonly IFileStorage _fileStorage;

        public CreateFileApplication(IFileRepository fileRepository, IFileStorage fileStorage)
        {
            _fileRepository = fileRepository;
            _fileStorage = fileStorage;
        }

        public async Task<Result<CreatedFile>> Execute(CreateFile createFile)
        {
            var maybeFile = await _fileRepository.FindOne(createFile.Id);

            if (maybeFile.HasValue)
            {
                return Result.Fail<CreatedFile>("Product with this id already exist");
            }

            var file = new File
            (
                createFile.Id,
                createFile.Name,
                createFile.Path,
                createFile.Ext
            );

            var createdFile = new CreatedFile
            {
                Id = file.Id,
                Name = file.Name,
                Ext = file.Ext,
                Path = file.Path,
                Content = createFile.Content
            };

            var result = await _fileRepository.Save(createdFile);

            if (result.Failure)
            {
                return Result.Fail<CreatedFile>(result.Message);
            }

            result = await _fileStorage.Save(createdFile);

            if (result.Failure)
            {
                return Result.Fail<CreatedFile>(result.Message);
            }

            return Result.Ok(createdFile);
        }
    }
}
