using ZPProductManagement.Common;
using ZPProductManagement.Common.Enums;
using ZPProductManagement.Domain.Entities;

namespace ZPProductManagement.Domain.ValueObjects
{
    internal class Activated : Status
    {
        public Activated(Product product) : base(product, EStatus.Activated)
        {
        }

        public override Result Active()
        {
            return Result.Fail("Product is already 'Activated'");
        }

        public override Result Archive()
        {
            Product.Status = Product.Archived;

            return Result.Ok();
        }

        public override Result Delete()
        {
            Product.Status = Product.Deleted;

            return Result.Ok();
        }
    }
}