namespace DddDemo.Entities
{
    /// <summary>
    /// https://enterprisecraftsmanship.com/posts/value-object-better-implementation/
    /// </summary>
    public abstract class ValueObject<T>
        where T : ValueObject<T>
    {
        public override bool Equals(object obj)
            => obj is T valueObject && (GetType() == obj.GetType() && EqualsCore(valueObject));

        protected abstract bool EqualsCore(T other);

        public override int GetHashCode()
            => GetHashCodeCore();

        protected abstract int GetHashCodeCore();

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
            => ReferenceEquals(a, null) && ReferenceEquals(b, null) ||
               !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.Equals(b);

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
            => !(a == b);
    }
}
