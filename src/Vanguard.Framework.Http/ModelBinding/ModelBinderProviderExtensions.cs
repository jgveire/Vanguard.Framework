﻿namespace Vanguard.Framework.Http.ModelBinding
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

    /// <summary>
    /// The model binder provider extensions.
    /// </summary>
    public static class ModelBinderProviderExtensions
    {
        /// <summary>
        /// Inserts the body and route binding into the model binder provider collection.
        /// </summary>
        /// <param name="providers">The model binder providers.</param>
        public static void InsertBodyAndRouteBinding(this IList<IModelBinderProvider> providers)
        {
            var bodyProvider = providers.Single(provider => provider.GetType() == typeof(BodyModelBinderProvider)) as BodyModelBinderProvider;
            var complexProvider = providers.Single(provider => provider.GetType() == typeof(ComplexTypeModelBinderProvider)) as ComplexTypeModelBinderProvider;
            var bodyAndRouteProvider = new BodyAndRouteModelBinderProvider(bodyProvider, complexProvider);

            providers.Insert(0, bodyAndRouteProvider);
        }
    }
}