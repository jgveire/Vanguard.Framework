using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
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
            var eventHandlers = new List<IEventHandler<TestEvent>>
            {
                new TestEventHandler()
            };

            // Arrange mocks
            Mocks<IServiceProvider>()
                .Setup(provider => provider.GetService(typeof(IEnumerable<IEventHandler<TestEvent>>)))
                .Returns(eventHandlers);

            // Act
            SystemUnderTest.Dispatch(domainEvent);

            // Assert
            domainEvent.IsHandlerExecuted.Should().BeTrue(because: "the test event handler changes the value of the test event");
        }

        [TestMethod]
        public void When_DispatchAsync_is_called_the_event_handler_should_set_the_value_to_true()
        {
            // Arrange
            var domainEvent = new TestEvent();
            var eventHandlers = new List<IEventHandler<TestEvent>>
            {
                new TestEventHandler()
            };

            // Arrange mocks
            Mocks<IServiceProvider>()
                .Setup(provider => provider.GetService(typeof(IEnumerable<IEventHandler<TestEvent>>)))
                .Returns(eventHandlers);

            // Act
            SystemUnderTest.Dispatch(domainEvent);

            // Assert
            domainEvent.IsHandlerExecuted.Should().BeTrue(because: "the test event handler changes the value of the test event");
        }
    }
}
