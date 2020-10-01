namespace DddWorkshops.Common.ModelFramework
{
    /// <summary>
    ///     Base class for all value objects.
    /// </summary>
    /// <typeparam name="T">Generic type of the value object.</typeparam>
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        public static bool operator ==(ValueObject<T>? first, ValueObject<T>? second)
        {
            if (first is null && second is null)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            return ReferenceEquals(first, second) || first.Equals(second);
        }

        public static bool operator !=(ValueObject<T>? first, ValueObject<T>? second) => !(first == second);

        /// <inheritdoc />
        public override bool Equals(object? obj) => obj is ValueObject<T> && InternalEquals((T)obj);

        /// <inheritdoc />
        public override int GetHashCode() => InternalGetHashCode();

        /// <summary>
        ///     Internal implementation for equality comparison.
        /// </summary>
        /// <param name="valueObject">Other value object to compare.</param>
        /// <returns>Boolean representing whether this object is equal to <paramref name="valueObject"/>.</returns>
        /// <remarks>
        ///     This method's implementations should implement 'MemberwiseEquals' pattern,
        ///     as value objects represent immutable values (and should be treated as structs instead of reference types).
        /// </remarks>
        protected abstract bool InternalEquals(T valueObject);

        /// <summary>
        ///     Internal implementation of determining value object's hash code.
        /// </summary>
        /// <returns>Hash code of the value object.</returns>
        /// <remarks>
        ///     This method's implementations should always combine hash codes of all members of a value object,
        ///     as value objects represent immutable values (and should be treated as structs instead of reference types).
        /// </remarks>
        protected abstract int InternalGetHashCode();
    }
}