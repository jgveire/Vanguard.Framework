namespace Vanguard.Framework.Http.Extensions
{
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// The header dictionary extensions.
    /// </summary>
    public static class HeaderDictionaryExtensions
    {
        /// <summary>
        /// Adds the total count to the header dictionary with the key X-Total-Count.
        /// </summary>
        /// <param name="source">The header  dictionary.</param>
        /// <param name="totalCount">The total count.</param>
        public static void AddTotalCount(this IHeaderDictionary source, int totalCount)
        {
            Guard.ArgumentNotNull(source, nameof(source));
            source.Add("X-Total-Count", totalCount.ToString());
        }
    }
}