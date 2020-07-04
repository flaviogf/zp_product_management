using System;
using System.Threading.Tasks;
using ZPProductManagement.Application.Products;
using ZPProductManagement.Common;

namespace ZPProductManagement.Application
{
    public interface IProductRepository
    {
        Task<Result> Save(CreatedProduct createdProduct);

        Task<Maybe<StoredProduct>> FindById(Guid id);
    }
}