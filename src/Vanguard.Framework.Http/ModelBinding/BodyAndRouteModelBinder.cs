namespace Vanguard.Framework.Http.ModelBinding
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// The body and route model binder.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder" />
    public class BodyAndRouteModelBinder : IModelBinder
    {
        private readonly IModelBinder _bodyBinder;
        private readonly IModelBinder _complexBinder;

        /// <summary>
        /// Initializes a new instance of the <see cref="BodyAndRouteModelBinder"/> class.
        /// </summary>
        /// <param name="bodyBinder">The body binder.</param>
        /// <param name="complexBinder">The complex binder.</param>
        public BodyAndRouteModelBinder(IModelBinder bodyBinder, IModelBinder complexBinder)
        {
            _bodyBinder = bodyBinder;
            _complexBinder = complexBinder;
        }

        /// <summary>
        /// Attempts to bind a model.
        /// </summary>
        /// <param name="bindingContext">The <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext" />.</param>
        /// <returns>
        /// <para>
        /// A <see cref="T:System.Threading.Tasks.Task" /> which will complete when the model binding process completes.
        /// </para>
        /// <para>
        /// If model binding was successful, the <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext.Result" /> should have
        /// <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingResult.IsModelSet" /> set to <c>true</c>.
        /// </para>
        /// <para>
        /// A model binder that completes successfully should set <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext.Result" /> to
        /// a value returned from <see cref="M:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingResult.Success(System.Object)" />.
        /// </para>
        /// </returns>
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            await _bodyBinder.BindModelAsync(bindingContext).ConfigureAwait(false);

            if (bindingContext.Result.IsModelSet)
            {
                bindingContext.Model = bindingContext.Result.Model;
            }

            await _complexBinder.BindModelAsync(bindingContext).ConfigureAwait(false);
        }
    }
}