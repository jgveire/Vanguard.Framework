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
        private static readonly Guid _categoryId = new Guid("9F7982B7-29CA-4671-ABF6-89C19EBEDAFB");

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

        [TestMethod]
        public void When_string_equal_filter_with_single_quote_is_passed_then_result_should_be_true()
        {
            // Arrange
            var systemUnderTest = new FilterParser<Product>("name eq  'She''s fun'");
            var items = GetProducts();

            // Act
            var result = systemUnderTest.ApplyFilter(items);

            // Assert
            result.Should().HaveCount(1, because: "we only have one product with the name 'She's fun'.");
        }

        [TestMethod]
        public void When_guid_equal_filter_is_passed_then_result_should_be_true()
        {
            // Arrange
            var systemUnderTest = new FilterParser<Product>("categoryId eq 9F7982B7-29CA-4671-ABF6-89C19EBEDAFB");
            var items = GetProducts();

            // Act
            var result = systemUnderTest.ApplyFilter(items);

            // Assert
            result.Should().HaveCount(5, because: "all products have the same category id.");
        }

        private static IQueryable<Product> GetProducts()
        {
            var items = new List<Product>
            {
                new Product(1, "Bike", 20, _categoryId),
                new Product(2, "Car", 2000, _categoryId),
                new Product(3, "Chair", 10, _categoryId),
                new Product(4, "Plane", 20000, _categoryId),
                new Product(5, "She's fun", 10, _categoryId),
            }.AsQueryable();
            return items;
        }
    }
}
