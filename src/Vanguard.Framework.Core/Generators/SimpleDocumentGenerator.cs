using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using Vanguard.Framework.Core.Extensions;

namespace Vanguard.Framework.Core.Generators
{
    /// <summary>
    /// A simple document generator.
    /// It does not support collections and complex properties.
    /// </summary>
    public class SimpleDocumentGenerator : IDocumentGenerator
    {
        private const string ForEachExpression = @"\{\{foreach [a-zA-Z0-9]+ in [a-zA-Z0-9]+\}\}";
        private const string IfExpression = @"\{\{if [a-zA-Z0-9]+\}\}";
        private const string EndPattern = @"{{end}}";

        /// <summary>
        /// The content tag which should be used in the layout tempalte.
        /// </summary>
        public const string ContentTag = "{{Content}}";

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleDocumentGenerator"/> class.
        /// </summary>
        /// <param name="documentType">The document type that is going to be generated.</param>
        public SimpleDocumentGenerator(DocumentType documentType = DocumentType.PlainText)
        {
            DocumentType = documentType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleDocumentGenerator"/> class.
        /// </summary>
        /// <param name="layoutTemplate">The layout template.</param>
        /// <param name="documentType">The document type that is going to be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when the layoutTemplate is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the layout template does not contain the tag {{content}}.</exception>
        public SimpleDocumentGenerator(string layoutTemplate, DocumentType documentType = DocumentType.PlainText)
        {
            Guard.ArgumentNotNull(layoutTemplate, nameof(layoutTemplate));
            if (!layoutTemplate.Contains(ContentTag, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("The layout must contain the tag {{content}}.", nameof(layoutTemplate));
            }

            DocumentType = documentType;
            LayoutTemplate = layoutTemplate;
        }

        /// <summary>
        /// Gets the document type.
        /// </summary>
        /// <value>
        /// The document type.
        /// </value>
        public DocumentType DocumentType { get; }

        /// <summary>
        /// Gets the layout template.
        /// </summary>
        /// <value>
        /// The layout template.
        /// </value>
        public string LayoutTemplate { get; }

        /// <summary>
        /// Generates a document from the specified template.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="template">The template that should be used for generation.</param>
        /// <param name="templateModel">The model that should be used for generation.</param>
        /// <returns>A document.</returns>
        public string Generate<TModel>(string template, TModel templateModel)
        {
            Guard.ArgumentNotNullOrEmpty(template, nameof(template));
            Guard.ArgumentNotNull(templateModel, nameof(templateModel));

            string result = template;
            IEnumerable<PropertyInfo> properties = GetModelProperties(templateModel);
            foreach (PropertyInfo property in properties)
            {
                string propertyTag = GetPropertyTag(property.Name);
                if (!IsPropertyTagInTemplateDefined(template, propertyTag))
                {
                    continue;
                }

                object value = property.GetValue(templateModel);
                result = ReplacePropertyTagWithValue(result, propertyTag, value);
            }

            result = InsertContentIntoLayout(result);
            return result;
        }

        private static string GetPropertyTag(string propertyName)
        {
            return "{{" + propertyName + "}}";
        }

        private static bool IsPropertyTagInTemplateDefined(string template, string propertyTag)
        {
            return template.Contains(propertyTag, StringComparison.OrdinalIgnoreCase);
        }

        private IEnumerable<PropertyInfo> GetModelProperties<TModel>(TModel templateModel)
        {
            var type = typeof(TModel);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return properties;
        }

        private string InsertContentIntoLayout(string content)
        {
            if (LayoutTemplate == null)
            {
                return content;
            }

            return ReplacePropertyTagWithValue(LayoutTemplate, "{{Content}}", content);
        }

        private string ReplacePropertyTagWithValue(string template, string propertyTag, object value)
        {
            string replaceString = string.Empty;
            if (value != null)
            {
                replaceString = value.ToString();
            }

            if (DocumentType == DocumentType.Html)
            {
                replaceString = HttpUtility.HtmlEncode(replaceString);
            }

            return Regex.Replace(template, Regex.Escape(propertyTag), replaceString, RegexOptions.IgnoreCase);
        }
    }
}
