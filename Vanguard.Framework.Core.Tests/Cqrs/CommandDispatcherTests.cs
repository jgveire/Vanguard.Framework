using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vanguard.Framework.Core.Cqrs;
using Vanguard.Framework.Test;

namespace Vanguard.Framework.Core.Tests.Cqrs
{
    [TestClass]
    public class CommandDispatcherTests : TestBase<CommandDispatcher>
    {
        [TestMethod]
        public void When_Dispatch_Is_Called_The_Command_Handler_Should_Be_Executed()
        {
            // Arrange
            var command = new TestCommand();
            var commandHandler = new TestCommandHandler();

            // Arrange mocks
            Mocks<IServiceProvider>()
                .Setup(provider => provider.GetService(typeof(ICommandHandler<TestCommand>)))
                .Returns(commandHandler);

            // Act
            Action action = () => SystemUnderTest.Dispatch(command);

            // Assert
            action.ShouldThrow<NotImplementedException>(because: "we did not implement the Execute method of the TestCommandHandler.");
        }
    }
}
