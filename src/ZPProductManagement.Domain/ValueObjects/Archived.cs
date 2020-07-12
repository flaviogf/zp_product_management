using ZPProductManagement.Common;
using ZPProductManagement.Common.Enums;
using ZPProductManagement.Domain.Entities;

namespace ZPProductManagement.Domain.ValueObjects
{
    internal class Archived : Status
    {
        public Archived(Product product) : base(product, EStatus.Archived)
        {
        }

        public override Result Active()
        {
            Product.Status = Product.Activated;

            return Result.Ok();
        }

        public override Result Archive()
        {
            return Result.Fail("Product is already 'Archived'");
        }

        public override Result Delete()
        {
            Product.Status = Product.Deleted;

            return Result.Ok();
        }
    }
}