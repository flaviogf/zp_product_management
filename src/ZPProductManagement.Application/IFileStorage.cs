using System.Threading.Tasks;
using ZPProductManagement.Common;

namespace ZPProductManagement.Application
{
    public interface IFileStorage
    {
        Task<Result> Save(CreatedFile createdFile);
    }
}
