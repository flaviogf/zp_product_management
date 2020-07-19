using ZPProductManagement.Common;
using ZPProductManagement.Common.Enums;
using ZPProductManagement.Domain.Entities;

namespace ZPProductManagement.Domain.ValueObjects
{
    public abstract class Status
    {
        protected Status(Product product, EStatus value)
        {
            Product = product;
            Value = value;
        }

        protected Product Product { get; }

        protected EStatus Value { get; }

        public abstract Result Active();

        public abstract Result Archive();

        public abstract Result Delete();

        public static implicit operator EStatus(Status status)
        {
            return status.Value;
        }
    }
}
