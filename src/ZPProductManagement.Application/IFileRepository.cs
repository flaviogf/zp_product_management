using System.Threading.Tasks;
using ZPProductManagement.Application.Files;
using ZPProductManagement.Common;

namespace ZPProductManagement.Application
{
    public interface IFileRepository
    {
        Task<Result> Save(CreatedFile file);

        Task<Maybe<StoredFile>> FindByName(string name);
    }
}