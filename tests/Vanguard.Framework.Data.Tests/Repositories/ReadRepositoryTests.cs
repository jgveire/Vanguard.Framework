namespace Vanguard.Framework.Data.Tests.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Vanguard.Framework.Core;
    using Vanguard.Framework.Data.Repositories;
    using Vanguard.Framework.Test;

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
        public void When_Find_is_called_then_collection_should_be_ordered_ascending()
        {
            // Arrange
            var searchCriteria = new FilterQuery
            {
                OrderBy = "name"
            };

            // Act
            var result = SystemUnderTest.Find(searchCriteria);

            // Assert
            result.First().Name.Should().Be("Apple", because: "the collection is ordered by product name ascending");
        }

        [TestMethod]
        public void When_Find_is_called_then_collection_should_be_ordered_descending()
        {
            // Arrange
            var searchCriteria = new FilterQuery
            {
                OrderBy = "name",
                SortOrder = SortOrder.Desc
            };

            // Act
            var result = SystemUnderTest.Find(searchCriteria);

            // Assert
            result.First().Name.Should().Be("How To Cook", because: "the collection is ordered by product name descending");
        }

        [TestMethod]
        public void When_Find_is_called_then_collection_should_be_paged()
        {
            // Arrange
            var searchCriteria = new FilterQuery
            {
                Page = 2,
                PageSize = 1
            };

            // Act
            var result = SystemUnderTest.Find(searchCriteria);

            // Assert
            result.Count().Should().Be(1, because: "the page size is 1");
            result.First().Name.Should().Be("How To Cook", because: "the page is 2, the page size is 1 and the product 'How To Cook' is the second item in the collection");
        }

        [TestMethod]
        public void When_Find_is_called_with_filter_then_collection_should_be_filtered()
        {
            // Arrange
            Expression<Func<Product, bool>> filter = product => product.Name == "Bear";

            // Act
            var result = SystemUnderTest.Find(null, filter);

            // Assert
            result.Count().Should().Be(1, because: "we have only 1 bear in our collection");
            result.First().Name.Should().Be("Bear", because: "the filter is that the name should be equal to Bear");
        }

        [TestMethod]
        public void When_Find_is_called_then_fields_should_be_selected()
        {
            // Arrange
            var searchCriteria = new FilterQuery
            {
                Select = "Id,Name"
            };

            // Act
            var result = SystemUnderTest.Find(searchCriteria);

            // Assert
            result.Any(product => product.Id == 0).Should().BeFalse(because: "we selected the field Id and Name");
            result.Any(product => product.Name == null).Should().BeFalse(because: "we selected the field Id and Name");
            result.All(product => product.Category == null).Should().BeTrue(because: "we selected the field Id and Name and not Category");
        }

        protected override Repository<Product> CreateSystemUnderTest()
        {
            return new Repository<Product>(new ProductContext());
        }
    }
}
