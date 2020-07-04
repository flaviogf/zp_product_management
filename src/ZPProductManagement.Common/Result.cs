using System.Collections.Generic;

namespace ZPProductManagement.Common
{
    public class Result
    {
        protected Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; }

        public string Message { get; }

        public bool Failure => !Success;

        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }

        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default, false, message);
        }

        public static Result Combine(params Result[] results)
        {
            foreach (var result in results)
            {
                if (result.Failure)
                {
                    return Fail(result.Message);
                }
            }

            return Ok();
        }

        public static Result Combine(IEnumerable<Result> results)
        {
            foreach (var result in results)
            {
                if (result.Failure)
                {
                    return Fail(result.Message);
                }
            }

            return Ok();
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
