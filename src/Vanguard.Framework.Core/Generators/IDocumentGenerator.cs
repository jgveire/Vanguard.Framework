namespace Vanguard.Framework.Core.Generators
{
    /// <summary>
    /// A document generator.
    /// </summary>
    public interface IDocumentGenerator
    {
        /// <summary>
        /// Gets the document type.
        /// </summary>
        /// <value>
        /// The document type.
        /// </value>
        DocumentType DocumentType { get; }

        /// <summary>
        /// Gets the layout template.
        /// </summary>
        /// <value>
        /// The layout template.
        /// </value>
        string LayoutTemplate { get; }

        /// <summary>
        /// Generates a document from the specified template.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="template">The template that should be used for generation.</param>
        /// <param name="templateModel">The model that should be used for generation.</param>
        /// <returns>A document.</returns>
        string Generate<TModel>(string template, TModel templateModel);
    }
}