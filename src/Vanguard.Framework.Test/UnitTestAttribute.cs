namespace Vanguard.Framework.Test
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The unit test category attribute class.
    /// </summary>
    /// <seealso cref="Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryBaseAttribute" />
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class UnitTestAttribute : TestCategoryBaseAttribute
    {
        /// <inheritdoc />
        public override IList<string> TestCategories { get; } = new List<string> { "UnitTest" };
    }
}
