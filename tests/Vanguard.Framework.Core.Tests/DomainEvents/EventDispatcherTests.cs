using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vanguard.Framework.Core.DomainEvents;
using Vanguard.Framework.Test;

namespace Vanguard.Framework.Core.Tests.DomainEvents
{
    [TestClass]
    public class EventDispatcherTests : TestBase<EventDispatcher>
    {
        [TestMethod]
        public void When_Dispatch_is_called_the_event_handler_should_set_the_value_to_true()
        {
            // Arrange
            var domainEvent = new TestEvent();
            var eventHandler = new TestEventHandler();

            // Arrange mocks
            Mocks<IServiceProvider>()
                .Setup(provider => provider.GetService(typeof(IEventHandler<TestEvent>)))
                .Returns(eventHandler);

            // Act
            SystemUnderTest.Dispatch(domainEvent);

            // Assert
            domainEvent.IsHandlerExecuted.Should().BeTrue(because: "the test event handler changes the value of the test event");
        }
    }
}
