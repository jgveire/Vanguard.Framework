namespace Vanguard.Framework.Data.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Vanguard.Framework.Core;

    [TestClass]
    public class QueryableExtensionsTests
    {
        private static IQueryable<Product>? _products;

        private static IQueryable<Product> Products => GetProducts();

        [TestMethod]
        public void When_OrderBy_is_called_then_apple_should_be_first_item_in_the_collection()
        {
            // Act
            var result = Data.Repositories.QueryableExtensions.OrderBy(Products, "Name");

            // Assert
            result.First().Name.Should().Be("Apple", because: "we ordered on the name property ascending");
        }

        [TestMethod]
        public void When_OrderBy_is_called_then_bike_should_be_first_item_in_the_collection()
        {
            // Act
            var result = Data.Repositories.QueryableExtensions.OrderBy(Products, "Category.Name");

            // Assert
            result.First().Name.Should().Contain("Bike", because: "we ordered on the category name property ascending");
        }

        [TestMethod]
        public void When_OrderByDescending_is_called_then_HowToProgram_should_be_first_item_in_the_collection()
        {
            // Act
            var result = Data.Repositories.QueryableExtensions.OrderByDescending(Products, "Name");

            // Assert
            result.First().Name.Should().Be("Mountain Bike", because: "we ordered on the name property descending");
        }

        [TestMethod]
        public void When_Search_is_called_then_apple_should_be_first_item_in_the_collection()
        {
            // Act
            var result = Data.Repositories.QueryableExtensions.Search(Products, "Appl");

            // Assert
            result.First().Name.Should().Be("Apple", because: "we have one item with the product name Apple");
        }

        [TestMethod]
        public void When_Search_is_called_then_bike_should_be_first_item_in_the_collection()
        {
            // Act
            var result = Data.Repositories.QueryableExtensions.Search(Products, "3");

            // Assert
            result.First().Name.Should().Be("BMX Bike", because: "we have one item with the product id 3");
        }

        [TestMethod]
        public void When_Select_is_called_then_only_id_should_be_set()
        {
            // Act
            var result = Data.Repositories.QueryableExtensions.Select(Products, "Id");

            // Assert
            result.Any(product => product.Id == 0).Should().BeFalse(because: "we selected the field Id");
            result.All(product => product.Name == string.Empty).Should().BeTrue(because: "we selected the field Id and not Name");
            result.All(product => product.Category == null).Should().BeTrue(because: "we selected the field Id and not Category");
        }

        [TestMethod]
        public void When_Validate_is_called_with_member_path_then_no_validation_exception_should_be_thrown()
        {
            // Arrange
            var filter = new OrderByFilter
            {
                OrderBy = "category.name",
            };

            // Act
            Action action = () => Data.Repositories.QueryableExtensions.Filter(Products, filter);

            // Assert
            action.Should().NotThrow(because: "it is allowed to order via member paths");
        }

        [TestMethod]
        public void When_a_property_is_mapped_no_order_by_validation_error_should_be_thrown()
        {
            // Arrange
            var filter = new OrderByFilter
            {
                OrderBy = "categoryName",
            };
            filter.PropertyMappings.Add("categoryName", "category.name");

            // Act
            Action action = () => Data.Repositories.QueryableExtensions.Filter(Products, filter);

            // Assert
            action.Should().NotThrow(because: "it is allowed to order via member paths");
        }

        private static IQueryable<Product> GetProducts()
        {
            if (_products == null)
            {
                var books = new ProductCategory(1, "Books");
                var computers = new ProductCategory(1, "Computers");
                var bikes = new ProductCategory(1, "Bikes");

                _products = new List<Product>
                {
                    new Product(1, "How to program", books),
                    new Product(2, "Apple", computers),
                    new Product(3, "BMX Bike", bikes),
                    new Product(4, "Mountain Bike", bikes),
                }.AsQueryable();
            }

            return _products;
        }
    }
}
