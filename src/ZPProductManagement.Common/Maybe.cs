namespace ZPProductManagement.Common
{
    public struct Maybe<T>
    {
        private Maybe(T value)
        {
            Value = value;
        }

        public bool HasValue => Value != null;

        public bool HasNoValue => !HasValue;

        public T Value { get; }

        public static implicit operator Maybe<T>(T value)
        {
            return new Maybe<T>(value);
        }
    }
}
