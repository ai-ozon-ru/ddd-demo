using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DddDemo.Entities
{
    public sealed class SkuList : ValueObject<SkuList>, IEnumerable<Sku>
    {
        private List<Sku> Items { get; }

        public SkuList(IEnumerable<Sku> items)
            => Items = new List<Sku>(items);

        /// <inheritdoc />
        public IEnumerator<Sku> GetEnumerator()
            => Items.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <inheritdoc />
        protected override bool EqualsCore(SkuList other)
            => Items
                .OrderBy(x => x.Name)
                .SequenceEqual(other.Items.OrderBy(x => x.Name));

        /// <inheritdoc />
        protected override int GetHashCodeCore()
            => Items.Count;

        /// <summary>
        /// Так как SkuList это Value object и он неизменяый,
        /// то если мы хотим добавить еще один элемент к нему
        /// необходимо создать новый SkuList
        /// </summary>
        public SkuList AddSku(string name, string category, string code)
        {
            var items = Items.ToList();
            items.Add(new Sku(name, category, code));

            return new SkuList(items);
        }

        public static explicit operator SkuList(string itemsList)
        {
            var items = itemsList.Split(';')
                .Select(x => (Sku)x)
                .ToList();

            return new SkuList(items);
        }

        public static implicit operator string(SkuList itemsList)
            => string.Join(";", itemsList.Select(x => (string)x));
    }
}
