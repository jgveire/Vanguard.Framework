using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VanguardWizard.Tests
{
    [TestClass]
    public class VanguardFrameworkWizardTests
    {
        [TestMethod]
        public void When_GetUserFriendlyName_is_called_then_a_friendly_name_should_be_returned()
        {
            // Arrange
            string value = "TestThis";

            // Act
            string result = VanguardFrameworkWizard.GetUserFriendlyName(value);

            // Assert
            result.Should().Be("test this", because: "capitals should be prefixed with a space");
        }
        
        [TestMethod]
        public void When_GetStrippedName_is_called_then_a_stripped_command_name_should_be_returned()
        {
            // Arrange
            string value = "TestCommandHandler";

            // Act
            string result = VanguardFrameworkWizard.GetStrippedName(value, "Command");

            // Assert
            result.Should().Be("Test", because: "the string Command and CommandHandler should be stripped from the input value");
        }

        [TestMethod]
        public void When_GetStrippedName_is_called_then_a_command_name_should_be_returned()
        {
            // Arrange
            string value = "Test";

            // Act
            string result = VanguardFrameworkWizard.GetStrippedName(value, "Command");

            // Assert
            result.Should().Be("Test", because: "the string Command and CommandHandler should be stripped from the input value");
        }

    }
}
