using System;
using System.Collections.Generic;
using System.Linq;
using Utils.Interface;

namespace Utils
{
    /// <summary>
    /// Implements the optional pattern
    /// </summary>
    /// <typeparam name="T">The type of the optional instance</typeparam>
    public class Optional<T> : IOptional<T>
    {
        private static readonly IOptional<T> noneInstance = new Optional<T>(Enumerable.Empty<T>());
        private static Action VoidAction { get; } = () => { /* do nothing */ };

        public static IOptional<T> None()
            => noneInstance;

        public static IOptional<T> Some(T value)
        {
            return value == null
                ? None()
                : new Optional<T>(new List<T> { value });
        }

        public static IOptional<T> Of(Func<T> func)
            => Optional<T>.Some(func());

        private List<T> Values { get; } = new List<T>();

        
        private Action ExecuteSome { get; set; } = VoidAction;
        private Func<T> ExecuteNone { get; set; } = () => default(T);

        private Optional(IEnumerable<T> value)
        {
            if (value == null) { throw new ArgumentNullException(nameof(value)); }
            if (value.Count() > 1) { throw new ArgumentException($"Collection '{nameof(value)}' contains more than one element"); }

            Values = new List<T>(value);
        }

        public IOptional<T> WhenSome()
            => WhenSome(VoidAction);

        public IOptional<T> WhenSome(Action action)
        {
            ExecuteSome = action ?? throw new ArgumentNullException(nameof(action));
            
            if (Values.Any())
            {
                ExecuteSome.Invoke();
            }
            return this;
        }

        public IOptional<T> WhenNone(Func<T> func)
        {
            ExecuteNone = func ?? throw new ArgumentNullException(nameof(func));
            
            if (!Values.Any())
            {
                ExecuteNone();
            }
            return this;
        }

        public IOptional<T> WhenSome(Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            return Values.Select(v => WhenSome(v, predicate))
                         .DefaultIfEmpty(None())
                         .Single();
            
        }
        private IOptional<T> WhenSome(T value, Func<T, bool> predicate)
            => predicate == null || predicate(value)
                ? new Optional<T>(new List<T> { value })
                : None();

        public T Map()
            => Values.Any()
                ?  Values.First()
                : ExecuteNone();
    }
}