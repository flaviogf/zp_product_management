using System.Threading.Tasks;
using ZPProductManagement.Application.Files;
using ZPProductManagement.Common;

namespace ZPProductManagement.Application
{
    public interface IFileRepository
    {
        Task<Result> Save(IFileAdapter file);

        Task<Maybe<IFileAdapter>> FindByName(string name);
    }
}
