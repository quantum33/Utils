using System;

namespace Utils.Interface
{
    public interface IOptional<T>
    {
        IOptional<T> WhenSome();
        IOptional<T> WhenSome(Func<T, bool> predicate);
        IOptional<T> WhenSome(Action action);
        IOptional<T> WhenNone(Func<T> func);
        T Map();
    }
}