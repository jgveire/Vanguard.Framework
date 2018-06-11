namespace Vanguard.Framework.Core.Tests.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Vanguard.Framework.Core.Parsers;

    [TestClass]
    public class FilterParserTests
    {
        [TestMethod]
        public void When_integer_equal_filter_is_passed_then_result_should_be_true()
        {
            // Arrange
            var systemUnderTest = new FilterParser<Product>("id eq 1");
            var items = GetProducts();

            // Act
            var result = systemUnderTest.ApplyFilter(items);

            // Assert
            result.Should().HaveCount(1, because: "we only have one product with the Id 1.");
        }

        [TestMethod]
        public void When_string_equal_filter_is_passed_then_result_should_be_true()
        {
            // Arrange
            var systemUnderTest = new FilterParser<Product>("name eq  'Car'");
            var items = GetProducts();

            // Act
            var result = systemUnderTest.ApplyFilter(items);

            // Assert
            result.Should().HaveCount(1, because: "we only have one product with the name 'Car'.");
        }

        private static IQueryable<Product> GetProducts()
        {
            var items = new List<Product>
            {
                new Product(1, "Bike", 20),
                new Product(2, "Car", 2000),
                new Product(3, "Chair", 10),
                new Product(4, "Plane", 20000),
            }.AsQueryable();
            return items;
        }
    }
}
