using System;
using System.Text.RegularExpressions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vanguard.Framework.Core.Cqrs;
using Vanguard.Framework.Test;

namespace Vanguard.Framework.Core.Tests.Cqrs
{
    [TestClass]
    public class QueryDispatcherTests : TestBase<QueryDispatcher>
    {
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
            result.Should().Be("Success", because: "the test query handler returns the string 'Success'");
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
            result.Should().Be("Success", because: "the test query handler returns the string 'Success'");
        }
    }
}
