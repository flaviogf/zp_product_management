using ZPProductManagement.Common;

namespace ZPProductManagement.Domain.ValueObjects
{
    public class Extension
    {
        private readonly string _value;

        private Extension(string value)
        {
            _value = value;
        }

        public static implicit operator string(Extension extension)
        {
            return extension._value;
        }

        public static Result<Extension> Of(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result.Fail<Extension>($"{nameof(value)} must be informed");
            }

            return Result.Ok(new Extension(value));
        }
    }
}
