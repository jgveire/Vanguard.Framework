using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vanguard.Framework.Data.Tests.Repositories
{
    [TestClass]
    public class QueryableExtensionsTests
    {
        private static IQueryable<Product> _products;

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

        private static IQueryable<Product> GetProducts()
        {
            if (_products == null)
            {
                _products = new List<Product>
                {
                    new Product(1, "How to program", "Books"),
                    new Product(2, "Apple", "Fruit"),
                    new Product(3, "BMX Bike", "Bikes"),
                    new Product(4, "Mountain Bike", "Bikes")
                }.AsQueryable();
            }

            return _products;
        }
    }
}
