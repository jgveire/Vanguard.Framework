namespace Vanguard.Framework.Http.Formatters
{
    using System.Buffers;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Newtonsoft.Json;
    using Vanguard.Framework.Http.Resolvers;

    /// <summary>
    /// The select field JSON output formatter.
    /// </summary>
    public class SelectFieldJsonOutputFormatter : JsonOutputFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectFieldJsonOutputFormatter"/> class.
        /// </summary>
        public SelectFieldJsonOutputFormatter()
            : base(JsonSerializerSettingsProvider.CreateSerializerSettings(), ArrayPool<char>.Shared)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectFieldJsonOutputFormatter"/> class.
        /// </summary>
        /// <param name="serializerSettings">
        /// The <see cref="JsonSerializerSettings"/>. Should be either the application-wide settings
        /// (<see cref="Microsoft.AspNetCore.Mvc.MvcJsonOptions.SerializerSettings"/>) or an instance
        /// <see cref="JsonSerializerSettingsProvider.CreateSerializerSettings"/> initially returned.
        /// </param>
        /// <param name="charPool">The <see cref="ArrayPool{Char}"/>.</param>
        public SelectFieldJsonOutputFormatter(JsonSerializerSettings serializerSettings, ArrayPool<char> charPool)
            : base(serializerSettings, charPool)
        {
        }

        /// <inheritdoc />
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            Guard.ArgumentNotNull(context, nameof(context));
            Guard.ArgumentNotNull(selectedEncoding, nameof(selectedEncoding));

            var response = context.HttpContext.Response;
            using (var writer = context.WriterFactory(response.Body, selectedEncoding))
            {
                WriteObject(writer, context.Object, context.HttpContext.Request.Query);

                // Perf: call FlushAsync to call WriteAsync on the stream with any content left in the TextWriter's
                // buffers. This is better than just letting dispose handle it (which would result in a synchronous
                // write).
                await writer.FlushAsync();
            }
        }

        /// <summary>
        /// Writes the given <paramref name="value"/> as JSON using the given
        /// <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">The <see cref="TextWriter"/> used to write the <paramref name="value"/></param>
        /// <param name="value">The value to write as JSON.</param>
        /// <param name="query">The query collection from the request.</param>
        protected void WriteObject(TextWriter writer, object value, IQueryCollection query)
        {
            Guard.ArgumentNotNull(writer, nameof(writer));

            using (var jsonWriter = CreateJsonWriter(writer))
            {
                var jsonSerializer = CreateJsonSerializer(query);
                jsonSerializer.Serialize(jsonWriter, value);
            }
        }

        /// <summary>
        /// Called during serialization to create the <see cref="JsonSerializer"/>.
        /// </summary>
        /// <param name="query">The query collection from the request.</param>
        /// <returns>The <see cref="JsonSerializer"/> used during serialization and deserialization.</returns>
        protected virtual JsonSerializer CreateJsonSerializer(IQueryCollection query)
        {
            SerializerSettings.ContractResolver = new SelectFieldContractResolver(query);
            return JsonSerializer.Create(SerializerSettings);
        }
    }
}
