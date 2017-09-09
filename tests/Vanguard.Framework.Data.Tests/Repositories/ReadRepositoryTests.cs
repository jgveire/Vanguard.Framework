using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vanguard.Framework.Core;
using Vanguard.Framework.Data.Repositories;
using Vanguard.Framework.Test;

namespace Vanguard.Framework.Data.Tests.Repositories
{
    [TestClass]
    public class ReadRepositoryTests : TestBase<Repository<Product>>
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            using (var context = new ProductContext())
            {
                context.Products.Add(new Product(1, "Bear", "Pets"));
                context.Products.Add(new Product(2, "How To Cook", "Book"));
                context.Products.Add(new Product(3, "Apple", "Food"));
                context.SaveChanges();
            }
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            base.TestCleanup();
            using (var context = new ProductContext())
            {
                foreach (var product in context.Products.ToList())
                {
                    context.Products.Remove(product);
                }

                context.SaveChanges();
            }
        }

        [TestMethod]
        public void When_Find_Is_Called_Then_Collection_Should_Be_Ordered_Ascending()
        {
            // Arrange
            var findCriteria = new FindCriteria
            {
                OrderBy = "name"
            };

            // Act
            var result = SystemUnderTest.Find(findCriteria);

            // Assert
            result.First().Name.Should().Be("Apple", because: "the collection is ordered by product name ascending");
        }

        [TestMethod]
        public void When_Find_Is_Called_Then_Collection_Should_Be_Ordered_Descending()
        {
            // Arrange
            var findCriteria = new FindCriteria
            {
                OrderBy = "name",
                SortOrder = SortOrder.Desc
            };

            // Act
            var result = SystemUnderTest.Find(findCriteria);

            // Assert
            result.First().Name.Should().Be("How To Cook", because: "the collection is ordered by product name descending");
        }

        [TestMethod]
        public void When_Find_Is_Called_Then_Collection_Should_Be_Paged()
        {
            // Arrange
            var findCriteria = new FindCriteria
            {
                Page = 2,
                PageSize = 1
            };

            // Act
            var result = SystemUnderTest.Find(findCriteria);

            // Assert
            result.Count().Should().Be(1, because: "the page size is 1");
            result.First().Name.Should().Be("How To Cook", because: "the page is 2, the page size is 1 and the product 'How To Cook' is the second item in the collection");
        }

        [TestMethod]
        [Ignore]
        public void When_Find_Is_Called_Then_Fields_Should_Be_Selected()
        {
            // Arrange
            var findCriteria = new FindCriteria
            {
                Select = "Id,Name"
            };

            // Act
            var result = SystemUnderTest.Find(findCriteria);

            // Assert
            result.Count().Should().Be(3, because: "we dit not supply a filter");
            result.First().Category.Should().BeNull(because: "we have only selected the Id and Name fields");
        }

        protected override Repository<Product> CreateSystemUnderTest()
        {
            return new Repository<Product>(new ProductContext());
        }
    }
}
