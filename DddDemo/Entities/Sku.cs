namespace DddDemo.Entities
{
    /// <summary>
    /// Артикул товара
    /// </summary>
    public sealed class Sku : ValueObject<Sku>
    {
        public Sku(string name, string category, string code)
        {
            Name = name;
            Category = category;
            Code = code;
        }

        /// <summary>
        /// Наименование товара
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Категория товара
        /// </summary>
        public string Category { get; }

        /// <summary>
        /// Код товара
        /// </summary>
        public string Code { get; }

        /// <inheritdoc />
        protected override bool EqualsCore(Sku other)
            => Name == other.Name
               && Category == other.Category
               && Code == other.Code;

        /// <inheritdoc />
        protected override int GetHashCodeCore()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ Category.GetHashCode();
                hashCode = (hashCode * 397) ^ Code.GetHashCode();
                return hashCode;
            }
        }

        public static explicit operator Sku(string v)
        {
            throw new System.NotImplementedException();
        }

        public static explicit operator string(Sku v)
        {
            throw new System.NotImplementedException();
        }
    }
}
