using $rootnamespace$.Queries;
using System;
using Vanguard.Framework.Core.Cqrs;

namespace $rootnamespace$.QueryHandlers
{
    /// <summary>
    /// The $userfriendlyqueryname$ query handler.
    /// </summary>
    public sealed class $queryname$QueryHandler : IQueryHandler<object, $queryname$Query>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="$queryname$QueryHandler"/> class.
        /// </summary>
        public $queryname$QueryHandler()
        {
        }

        /// <inheritdoc />
        public object Retrieve($queryname$Query query)
        {
            throw new NotImplementedException();
        }
    }
}
