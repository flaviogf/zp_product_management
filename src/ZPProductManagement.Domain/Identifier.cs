using System;
using ZPProductManagement.Common;

namespace ZPProductManagement.Domain
{
    public class Identifier
    {
        private readonly Guid _value;

        private Identifier(Guid value)
        {
            _value = value;
        }

        public static implicit operator Guid(Identifier identifier)
        {
            return identifier._value;
        }

        public static Result<Identifier> Of(Guid value)
        {
            if (value == Guid.Empty)
            {
                return Result.Fail<Identifier>($"{nameof(value)} must not be empty");
            }

            return Result.Ok(new Identifier(value));
        }
    }
}
