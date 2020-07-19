using System.Threading.Tasks;
using ZPProductManagement.Application.Categories;
using ZPProductManagement.Common;

namespace ZPProductManagement.Application
{
    public interface ICategoryRepository
    {
        Task<Maybe<ICategoryAdapter>> FindByName(string name);
    }
}
