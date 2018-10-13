namespace Vanguard.Framework.Core.Tests.Extensions
{
    using Core.Extensions;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StringExtensionsTests
    {
        [DataTestMethod]
        [DataRow("test", "Test")]
        [DataRow("123", "123")]
        [DataRow("test string", "Test string")]
        [DataRow("test.string", "Test.string")]
        public void When_integer_equal_filter_is_passed_then_result_should_be_true(string input, string output)
        {
            // Act
            var result = input.Capitalize();

            // Assert
            result.Should().Be(output);
        }
    }
}
