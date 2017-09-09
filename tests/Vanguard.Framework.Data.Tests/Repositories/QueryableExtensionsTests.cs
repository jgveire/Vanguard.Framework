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
        public void When_OrderBy_Is_Called_Then_Apple_Should_Be_First_Item_In_The_Collection()
        {
            // Act
            var result = Data.Repositories.QueryableExtensions.OrderBy(Products, "Name");

            // Assert
            result.First().Name.Should().Be("Apple", because: "we ordered on the name property ascending");
        }

        [TestMethod]
        public void When_OrderByDescending_Is_Called_Then_HowToProgram_Should_Be_First_Item_In_The_Collection()
        {
            // Act
            var result = Data.Repositories.QueryableExtensions.OrderByDescending(Products, "Name");

            // Assert
            result.First().Name.Should().Be("Mountain Bike", because: "we ordered on the name property descending");
        }

        [TestMethod]
        public void When_Search_Is_Called_Then_Apple_Should_Be_First_Item_In_The_Collection()
        {
            // Act
            var result = Data.Repositories.QueryableExtensions.Search(Products, "Appl");

            // Assert
            result.First().Name.Should().Be("Apple", because: "we have one item with the product name Apple");
        }

        [TestMethod]
        public void When_Search_Is_Called_Then_Bike_Should_Be_First_Item_In_The_Collection()
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
