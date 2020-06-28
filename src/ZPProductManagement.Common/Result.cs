namespace ZPProductManagement.Common
{
    public class Result
    {
        protected Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Failure => !Success;

        public bool Success { get; }

        public string Message { get; }

        public static Result Ok()
        {
            return new Result(true, default);
        }

        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, default);
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default, false, message);
        }
    }

    public class Result<T> : Result
    {
        public Result(T value, bool success, string message) : base(success, message)
        {
            Value = value;
        }

        public T Value { get; }
    }
}
