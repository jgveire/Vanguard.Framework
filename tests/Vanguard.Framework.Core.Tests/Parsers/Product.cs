namespace Vanguard.Framework.Core.Tests.Parsers
{
    using System;

    public class Product
    {
        public Product()
        {
        }

        public Product(int id, string name, decimal price, Guid categoryId)
        {
            Id = id;
            Name = name;
            Price = price;
            CategoryId = categoryId;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }
    }
}
