using ZPProductManagement.Common;

namespace ZPProductManagement.Domain.ValueObjects
{
    public class Quantity
    {
        private readonly int _value;

        private Quantity(int value)
        {
            _value = value;
        }

        public static implicit operator int(Quantity quantity)
        {
            return quantity._value;
        }

        public static Result<Quantity> Of(int value)
        {
            if (value < 0)
            {
                return Result.Fail<Quantity>($"{nameof(value)} must be greater or equal zero");
            }

            return Result.Ok(new Quantity(value));
        }
    }
}
