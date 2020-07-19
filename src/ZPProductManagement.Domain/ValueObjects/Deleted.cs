using ZPProductManagement.Common;
using ZPProductManagement.Common.Enums;
using ZPProductManagement.Domain.Entities;

namespace ZPProductManagement.Domain.ValueObjects
{
    internal class Deleted : Status
    {
        public Deleted(Product product) : base(product, EStatus.Deleted)
        {
        }

        public override Result Active()
        {
            Product.Status = Product.Activated;

            return Result.Ok();
        }

        public override Result Archive()
        {
            Product.Status = Product.Archived;

            return Result.Ok();
        }

        public override Result Delete()
        {
            return Result.Fail("Product is already 'Deleted'");
        }
    }
}
