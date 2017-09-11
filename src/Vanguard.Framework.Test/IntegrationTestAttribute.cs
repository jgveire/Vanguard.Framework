using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vanguard.Framework.Test
{
    /// <summary>
    /// The integration test category attribute class.
    /// </summary>
    /// <seealso cref="Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryBaseAttribute" />
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class IntegrationTestAttribute : TestCategoryBaseAttribute
    {
        /// <inheritdoc />
        public override IList<string> TestCategories { get; } = new List<string> { "Integration" };
    }
}
