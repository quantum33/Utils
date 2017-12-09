using System;
using System.Collections.Generic;

namespace Utils
{
    /// <summary>
    /// Implements the optional pattern
    /// </summary>
    /// <typeparam name="T">The type of the optional instance</typeparam>
    public struct Optional<T>
        where T : class
    {
        public static explicit operator T(Optional<T> optional)
            => optional.Value;

        public static implicit operator Optional<T>(T value)
            => new Optional<T>(value);

        private readonly T _value;
        public bool HasValue { get; }
        public T Value
            => HasValue ? _value : throw new InvalidOperationException("Use the 'HasValue' property before using the 'Value' property");

        public Optional(T value)
        {
            HasValue = value != null;
            _value = value;
        }

        public override bool Equals(object obj)
            => obj is Optional<T> optional && Equals(optional);

        public bool Equals(Optional<T> other)
        {
            if (HasValue != other.HasValue)
                return false;
            return HasValue && _value.Equals(other._value);
        }

        public override int GetHashCode()
        {
            var hashCode = -254034551;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + HasValue.GetHashCode();
            if (HasValue)
            {
                hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(Value);
            }
            return hashCode;
        }
    }
}