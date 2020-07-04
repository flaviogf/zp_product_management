using System.Threading.Tasks;
using ZPProductManagement.Application.Categories;
using ZPProductManagement.Common;

namespace ZPProductManagement.Application
{
    public interface ICategoryRepository
    {
        Task<Maybe<StoredCategory>> FindByName(string name);
    }
}