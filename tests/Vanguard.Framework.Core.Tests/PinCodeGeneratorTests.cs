namespace Vanguard.Framework.Core.Tests
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Vanguard.Framework.Test;

    [TestClass]
    public class PinCodeGeneratorTests : TestBase<PinCodeGenerator>
    {
        [TestMethod]
        public void When_generate_is_called_a_pin_code_should_be_generated()
        {
            // Act
            string result = SystemUnderTest.Generate(10);

            // Assert
            result.Should().NotBeNullOrEmpty(because: "we supplied the generate method with a length of 10");
            result.Should().HaveLength(10, because: "we supplied the generate method with a length of 10");
        }
    }
}
