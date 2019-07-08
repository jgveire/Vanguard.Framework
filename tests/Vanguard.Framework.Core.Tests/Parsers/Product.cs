namespace Vanguard.Framework.Core.Tests.Parsers
{
    using System;

    public class Product
    {
        public Product(int id, string name, decimal price, Guid categoryId)
        {
            Id = id;
            Name = name;
            Price = price;
            CategoryId = categoryId;
        }

        protected Product()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public Guid CategoryId { get; set; } = Guid.Empty;
    }
}
