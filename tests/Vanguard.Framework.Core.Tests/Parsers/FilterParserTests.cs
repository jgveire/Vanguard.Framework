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
        public void When_empty_string_equal_filter_is_passed_then_result_should_be_zero()
        {
            // Arrange
            var systemUnderTest = new FilterParser<Product>("name eq ''");
            var items = GetProducts();

            // Act
            var result = systemUnderTest.ApplyFilter(items);

            // Assert
            result.Should().HaveCount(0, because: "there are no product names with an empty string");
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

        [TestMethod]
        public void When_string_like_filter_is_passed_then_result_should_have_two_results()
        {
            // Arrange
            var systemUnderTest = new FilterParser<Product>("name like 'C'");
            var items = GetProducts();

            // Act
            var result = systemUnderTest.ApplyFilter(items);

            // Assert
            result.Should().HaveCount(2, because: "there are two products with the letter C in the name.");
        }

        [TestMethod]
        public void When_string_like_filter_is_passed_then_result_should_have_no_results()
        {
            // Arrange
            var systemUnderTest = new FilterParser<Product>("name like 'C r'");
            var items = GetProducts();

            // Act
            var result = systemUnderTest.ApplyFilter(items);

            // Assert
            result.Should().HaveCount(0, because: "there are no products with the name 'C r'.");
        }

        [TestMethod]
        public void When_empty_string_like_filter_is_passed_then_all_results_should_be_returned()
        {
            // Arrange
            var systemUnderTest = new FilterParser<Product>("name like ''");
            var items = GetProducts();

            // Act
            var result = systemUnderTest.ApplyFilter(items);

            // Assert
            result.Should().HaveCount(5, because: "all product names contain an empty string.");
        }

        [TestMethod]
        public void When_int_like_filter_is_passed_then_an_exeption_should_be_thrown()
        {
            // Arrange
            var systemUnderTest = new FilterParser<Product>("price like 2");
            var items = GetProducts();

            // Act
            Action action = () => systemUnderTest.ApplyFilter(items);

            // Assert
            action.Should().Throw<FormatException>(because: "the like operator only works for string properties.");
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
