using ZPProductManagement.Common;

namespace ZPProductManagement.Domain
{
    public class Path
    {
        private readonly string _value;

        private Path(string value)
        {
            _value = value;
        }

        public static implicit operator string(Path path)
        {
            return path._value;
        }

        public static Result<Path> Of(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result.Fail<Path>($"{nameof(value)} must be informed");
            }

            return Result.Ok(new Path(value));
        }
    }
}