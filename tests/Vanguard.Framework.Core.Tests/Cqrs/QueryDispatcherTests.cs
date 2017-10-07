using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vanguard.Framework.Core.Cqrs;
using Vanguard.Framework.Test;

namespace Vanguard.Framework.Core.Tests.Cqrs
{
    [TestClass]
    public class QueryDispatcherTests : TestBase<QueryDispatcher>
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
        public void When_Dispatch_is_called_the_query_handler_should_return_success_string()
        {
            // Arrange
            var query = new TestQuery();
            var queryHandler = new TestQueryHandler();

            // Arrange mocks
            Mocks<IServiceProvider>()
                .Setup(provider => provider.GetService(typeof(IQueryHandler<string, TestQuery>)))
                .Returns(queryHandler);

            // Act
            string result = SystemUnderTest.Dispatch(query);

            // Assert
            result.Should().Be("Success", because: "the query handler returns the string 'Success'");
        }

        [TestMethod]
        public void When_Dispatch_is_called_explicitly_the_query_handler_should_return_success_string()
        {
            // Arrange
            var query = new TestQuery();
            var queryHandler = new TestQueryHandler();

            // Arrange mocks
            Mocks<IServiceProvider>()
                .Setup(provider => provider.GetService(typeof(IQueryHandler<string, TestQuery>)))
                .Returns(queryHandler);

            // Act
            string result = SystemUnderTest.Dispatch<string, TestQuery>(query);

            // Assert
            result.Should().Be("Success", because: "the query handler returns the string 'Success'");
        }

        [TestMethod]
        public void When_DispatchAsync_is_called_the_query_handler_should_return_success_string()
        {
            // Arrange
            var query = new TestAsyncQuery();
            var queryHandler = new TestAsyncQueryHandler();

            // Arrange mocks
            Mocks<IServiceProvider>()
                .Setup(provider => provider.GetService(typeof(IAsyncQueryHandler<string, TestAsyncQuery>)))
                .Returns(queryHandler);

            // Act
            string result = SystemUnderTest.DispatchAsync(query).Result;

            // Assert
            result.Should().Be("Success", because: "the query handler returns the string 'Success'");
        }

        [TestMethod]
        public void When_DispatchAsync_is_called_explicitly_the_query_handler_should_return_success_string()
        {
            // Arrange
            var query = new TestAsyncQuery();
            var queryHandler = new TestAsyncQueryHandler();

            // Arrange mocks
            Mocks<IServiceProvider>()
                .Setup(provider => provider.GetService(typeof(IAsyncQueryHandler<string, TestAsyncQuery>)))
                .Returns(queryHandler);

            // Act
            string result = SystemUnderTest.DispatchAsync<string, TestAsyncQuery>(query).Result;

            // Assert
            result.Should().Be("Success", because: "the query handler returns the string 'Success'");
        }
    }
}
