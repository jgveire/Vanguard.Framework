namespace Vanguard.Framework.Core.Tests
{
    using System.Web;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Vanguard.Framework.Test;

    [TestClass]
    public class UrlTokenGeneratorTests : TestBase<UrlTokenGenerator>
    {
        [TestMethod]
        public void When_generate_is_called_a_token_should_be_generated()
        {
            // Act
            var result = SystemUnderTest.Generate(100);
            var encodedString = HttpUtility.UrlEncode(result);

            // Assert
            result.Should().NotBeNullOrEmpty(because: "we supplied the generate method with a length of 100");
            result.Should().HaveLength(100, because: "we supplied the generate method with a length of 100");
            result.Should().Be(encodedString, because: "the token generator should only generate URL save tokens");
        }
    }
}
