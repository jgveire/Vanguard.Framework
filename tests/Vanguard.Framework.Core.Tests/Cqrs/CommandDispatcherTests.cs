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
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            base.TestCleanup();
        }

        [TestMethod]
        public void When_Dispatch_is_called_the_command_handler_should_be_executed()
        {
            // Arrange
            var command = new TestCommand();
            var commandHandler = new TestCommandHandler();

            // Arrange mocks
            Mocks<IServiceProvider>()
                .Setup(provider => provider.GetService(typeof(ICommandHandler<TestCommand>)))
                .Returns(commandHandler);

            // Act
            SystemUnderTest.Dispatch(command);

            // Assert
            command.IsHandlerExecuted.Should().BeTrue(because: "the command handler changes the value of the test command");
        }

        [TestMethod]
        public void When_DispatchAsync_is_called_the_command_handler_should_be_executed()
        {
            // Arrange
            var command = new TestAsyncCommand();
            var commandHandler = new TestAsyncCommandHandler();

            // Arrange mocks
            Mocks<IServiceProvider>()
                .Setup(provider => provider.GetService(typeof(IAsyncCommandHandler<TestAsyncCommand>)))
                .Returns(commandHandler);

            // Act
            SystemUnderTest.DispatchAsync(command).Wait();

            // Assert
            command.IsHandlerExecuted.Should().BeTrue(because: "the command handler changes the value of the test command");
        }
    }
}
