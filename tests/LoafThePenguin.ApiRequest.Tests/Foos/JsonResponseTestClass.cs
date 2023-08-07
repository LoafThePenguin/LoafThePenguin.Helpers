using System.Diagnostics.CodeAnalysis;

namespace LoafThePenguin.ApiRequest.Tests.Foos;

public sealed class JsonResponseTestClass
{
    public sealed class JsonResponseTestClassComparer<T> : IEqualityComparer<T>
    {
        private sealed class PrivateFooClassEqualityComparer : IEqualityComparer<JsonResponseTestClass>
        {
            public bool Equals(JsonResponseTestClass? x, JsonResponseTestClass? y)
            {
                return x.MyProperty1 == y.MyProperty1
                    && x.MyProperty == y.MyProperty
                    && x.MyProperty2 == y.MyProperty2;
            }

            public int GetHashCode([DisallowNull] JsonResponseTestClass obj)
            {
                return HashCode.Combine(obj.MyProperty, obj.MyProperty1, obj.MyProperty2);
            }
        }

        private readonly PrivateFooClassEqualityComparer _fooClassEqualityComparer;

        public JsonResponseTestClassComparer()
        {
            _fooClassEqualityComparer = new PrivateFooClassEqualityComparer();
        }

        public bool Equals(T? x, T? y)
        {
            if (x is JsonResponseTestClass xFoo && y is JsonResponseTestClass yFoo)
            {
                if (_fooClassEqualityComparer.GetHashCode(xFoo) != _fooClassEqualityComparer.GetHashCode(yFoo))
                {
                    return false;
                }

                return _fooClassEqualityComparer.Equals(xFoo, yFoo);
            }

            if (x is IEnumerable<JsonResponseTestClass> xEnumerable && y is IEnumerable<JsonResponseTestClass> yEnumerable)
            {
                return xEnumerable.SequenceEqual(yEnumerable, _fooClassEqualityComparer);
            }

            return false;
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            if (obj is JsonResponseTestClass foo)
            {
                return _fooClassEqualityComparer.GetHashCode(foo);
            }

            return 0;
        }
    }

    public int MyProperty { get; set; }
    public string MyProperty1 { get; set; }
    public bool MyProperty2 { get; set; }
}
