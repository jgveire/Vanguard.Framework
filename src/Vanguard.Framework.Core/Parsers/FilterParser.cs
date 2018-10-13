namespace Vanguard.Framework.Core.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using Extensions;

    /// <summary>
    /// The filter parser class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class FilterParser<TEntity>
    {
        private const char Seperator = ' ';
        private const char StringIndicator = '\'';

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterParser{TEntity}"/> class.
        /// </summary>
        /// <param name="filter">The filter to parse.</param>
        public FilterParser(string filter)
        {
            Filter = filter;
        }

        /// <summary>
        /// Gets the filter to parse.
        /// </summary>
        /// <value>
        /// The filter to parse.
        /// </value>
        public string Filter { get; }

        /// <summary>
        /// Applies the filter.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>A filtered collection.</returns>
        /// <exception cref="FormatException">Thrown when the filter is not in the correct format.</exception>
        public IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> source)
        {
            var entries = GetEntries();
            if (entries.Length != 3)
            {
                throw new FormatException("The filter should contain 3 elements, for example \"id eq 1\" or \"name = 'beer'\"");
            }
            else if (!string.Equals(entries[1], EqualParser.Operator, StringComparison.OrdinalIgnoreCase) &&
                     !string.Equals(entries[1], LikeParser.Operator, StringComparison.OrdinalIgnoreCase))
            {
                throw new FormatException("Only the 'eq' (equel) and 'like' filter are supported.");
            }

            IOperatorParser parser;
            var parameter = Expression.Parameter(typeof(TEntity), "item");
            if (string.Equals(entries[1], EqualParser.Operator, StringComparison.OrdinalIgnoreCase))
            {
                parser = new EqualParser(entries[0], entries[2]);
            }
            else
            {
                parser = new LikeParser(entries[0], entries[2]);
            }

            var equalExpression = parser.CreateExpression<TEntity>(parameter);
            var methodCall = Expression.Call(
                typeof(Queryable),
                "Where",
                new[] { parameter.Type },
                source.Expression,
                Expression.Lambda(equalExpression, parameter));
            return (IQueryable<TEntity>)source.Provider.CreateQuery(methodCall);
        }

        private string[] GetEntries()
        {
            var items = new List<string>();
            var sb = new StringBuilder();
            var foundString = false;
            for (int i = 0; i < Filter.Length; i++)
            {
                char curr = Filter[i];
                char? prev = Filter.Previous(i);
                char? next = Filter.Next(i);

                if (curr == Seperator && !foundString)
                {
                    if (sb.Length > 0)
                    {
                        items.Add(sb.ToString());
                    }

                    sb.Clear();
                }
                else if (curr == StringIndicator && foundString)
                {
                    if (next == StringIndicator)
                    {
                        sb.Append(curr);
                    }
                    else if (prev != StringIndicator)
                    {
                        items.Add(sb.ToString());
                        sb.Clear();
                        foundString = false;
                    }
                }
                else
                {
                    if (sb.Length == 0 && curr == StringIndicator)
                    {
                        foundString = true;
                    }
                    else
                    {
                        sb.Append(curr);
                    }
                }
            }

            if (sb.Length != 0 || foundString)
            {
                items.Add(sb.ToString());
            }

            return items.ToArray();
        }
    }
}
