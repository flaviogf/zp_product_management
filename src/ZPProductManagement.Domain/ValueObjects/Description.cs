using ZPProductManagement.Common;

namespace ZPProductManagement.Domain.ValueObjects
{
    public class Description
    {
        private readonly string _value;

        private Description(string value)
        {
            _value = value;
        }

        public static implicit operator string(Description description)
        {
            return description._value;
        }

        public static Result<Description> Of(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result.Fail<Description>($"{nameof(value)} must be informed");
            }

            return Result.Ok(new Description(value));
        }
    }
}
