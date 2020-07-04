using System;

namespace ZPProductManagement.Common
{
    public struct Maybe<T>
    {
        private readonly T _value;

        public Maybe(T value)
        {
            _value = value;
        }

        public T Value => _value ?? throw new InvalidOperationException();

        public bool HasValue => _value != null;

        public bool HasNoValue => !HasValue;

        public static implicit operator Maybe<T>(T value)
        {
            return new Maybe<T>(value);
        }
    }
}