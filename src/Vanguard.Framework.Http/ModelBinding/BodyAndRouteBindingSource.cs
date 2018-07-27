namespace Vanguard.Framework.Http.ModelBinding
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// The body and route binding source.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ModelBinding.BindingSource" />
    public class BodyAndRouteBindingSource : BindingSource
    {
        /// <summary>
        /// The body and route bind source instance.
        /// </summary>
        public static readonly BindingSource BodyAndRoute = new BodyAndRouteBindingSource(
            "BodyAndRoute",
            "BodyAndRoute",
            true,
            true);

        /// <summary>
        /// Initializes a new instance of the <see cref="BodyAndRouteBindingSource"/> class.
        /// </summary>
        /// <param name="id">The id, a unique identifier.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="isGreedy">A value indicating whether or not the source is greedy.</param>
        /// <param name="isFromRequest">A value indicating whether or not the data comes from the HTTP request.</param>
        public BodyAndRouteBindingSource(string id, string displayName, bool isGreedy, bool isFromRequest)
            : base(id, displayName, isGreedy, isFromRequest)
        {
        }

        /// <summary>
        /// Gets a value indicating whether or not the <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.BindingSource" /> can accept
        /// data from <paramref name="bindingSource" />.
        /// </summary>
        /// <param name="bindingSource">The <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.BindingSource" /> to consider as input.</param>
        /// <returns>
        ///   <c>True</c> if the source is compatible, otherwise <c>false</c>.
        /// </returns>
        /// <remarks>
        /// When using this method, it is expected that the left-hand-side is metadata specified
        /// on a property or parameter for model binding, and the right hand side is a source of
        /// data used by a model binder or value provider.
        /// This distinction is important as the left-hand-side may be a composite, but the right
        /// may not.
        /// </remarks>
        public override bool CanAcceptDataFrom(BindingSource bindingSource)
        {
            return bindingSource == Body || bindingSource == this;
        }
    }
}
