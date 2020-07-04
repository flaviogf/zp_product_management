using System;
using System.Threading.Tasks;
using ZPProductManagement.Application.Products;
using ZPProductManagement.Common;

namespace ZPProductManagement.Application
{
    public interface IProductRepository
    {
        Task<Result> Save(IProductAdapter productAdapter);

        Task<Maybe<IProductAdapter>> FindById(Guid id);
    }
}