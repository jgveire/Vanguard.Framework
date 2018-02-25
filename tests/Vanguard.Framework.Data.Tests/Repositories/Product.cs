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

        public Product(int id, string name, string category)
        {
            Id = id;
            Name = name;
            Category = category;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Category { get; set; }

        [Required]
        public DateTime StockDate { get; set; }
    }
}
