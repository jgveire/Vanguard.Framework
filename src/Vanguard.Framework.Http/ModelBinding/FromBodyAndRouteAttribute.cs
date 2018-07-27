namespace Vanguard.Framework.Http.ModelBinding
{
    using System;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// The from body and route attribute.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ModelBinding.IBindingSourceMetadata" />
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FromBodyAndRouteAttribute : Attribute, IBindingSourceMetadata
    {
        /// <summary>
        /// Gets the <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.IBindingSourceMetadata.BindingSource" />.
        /// </summary>
        /// <remarks>
        /// The <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.IBindingSourceMetadata.BindingSource" /> is metadata which can be used to determine which data
        /// sources are valid for model binding of a property or parameter.
        /// </remarks>
        public BindingSource BindingSource => BodyAndRouteBindingSource.BodyAndRoute;
    }
}