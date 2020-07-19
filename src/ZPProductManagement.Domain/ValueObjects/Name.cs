using ZPProductManagement.Common;

namespace ZPProductManagement.Domain.ValueObjects
{
    public class Name
    {
        private readonly string _value;

        private Name(string value)
        {
            _value = value;
        }

        public static implicit operator string(Name name)
        {
            return name._value;
        }

        public static Result<Name> Of(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result.Fail<Name>($"{nameof(value)} must be informed");
            }

            return Result.Ok(new Name(value));
        }
    }
}
