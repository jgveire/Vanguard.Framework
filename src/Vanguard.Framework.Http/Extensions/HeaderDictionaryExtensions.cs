namespace Vanguard.Framework.Http.Extensions
{
    using System.Net.Mime;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// The header dictionary extensions.
    /// </summary>
    public static class HeaderDictionaryExtensions
    {
        /// <summary>
        /// Adds a content disposition header to the header dictionary.
        /// </summary>
        /// <param name="source">The header  dictionary.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="displayInline">if set to <c>true</c> the file should be displayed in line.</param>
        public static void AddContentDisposition(this IHeaderDictionary source, string fileName, bool displayInline = true)
        {
            Guard.ArgumentNotNull(source, nameof(source));
            var contentDisposition = new ContentDisposition
            {
                FileName = fileName,
                Inline = displayInline
            };
            source.Add("Content-Disposition", contentDisposition.ToString());
        }

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