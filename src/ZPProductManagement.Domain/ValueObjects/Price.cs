using ZPProductManagement.Common;

namespace ZPProductManagement.Domain.ValueObjects
{
    public class Price
    {
        public readonly decimal _value;

        private Price(decimal value)
        {
            _value = value;
        }

        public static implicit operator decimal(Price price)
        {
            return price._value;
        }

        public static Result<Price> Of(decimal value)
        {
            if (value <= 0)
            {
                return Result.Fail<Price>($"{nameof(value)} must be greater than zero");
            }

            return Result.Ok(new Price(value));
        }
    }
}