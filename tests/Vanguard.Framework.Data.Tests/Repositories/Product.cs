namespace Vanguard.Framework.Data.Tests.Repositories
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Vanguard.Framework.Core.Repositories;

    public class Product : IDataEntity
    {
        public Product()
        {
        }

        public Product(int id, string name, int categoryId)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
        }

        internal Product(int id, string name, ProductCategory category)
        {
            Id = id;
            Name = name;
            CategoryId = category.Id;
            Category = category;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public ProductCategory Category { get; set; }

        [Required]
        public DateTime StockDate { get; set; }
    }
}
