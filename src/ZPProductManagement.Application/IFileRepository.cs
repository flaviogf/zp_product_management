using System;
using System.Threading.Tasks;
using ZPProductManagement.Common;

namespace ZPProductManagement.Application
{
    public interface IFileRepository
    {
        Task<Result> Save(CreatedFile createdFile);

        Task<Maybe<StoredFile>> FindOne(Guid id);
    }
}
