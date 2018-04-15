namespace Vanguard.Framework.Data.Tests.Repositories
{
    using System.ComponentModel.DataAnnotations;
    using Vanguard.Framework.Core.Repositories;

    public class ProductCategory : IDataEntity
    {
        public ProductCategory()
        {
        }

        public ProductCategory(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
