using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Vanguard.Framework.Test
{
    /// <summary>
    /// The test base class.
    /// </summary>
    [TestClass]
    public class TestBase
    {
        /// <summary>
        /// Gets the mock repository.
        /// </summary>
        /// <value>
        /// The mock repository.
        /// </value>
        protected MockRepository MockRepository { get; private set; }

        /// <summary>
        /// Initializes each test and therefor is run before each test.
        /// </summary>
        [TestInitialize]
        public virtual void TestInitialize()
        {
            MockRepository = new MockRepository(MockBehavior.Strict);
        }

        /// <summary>
        /// Cleans up after each test and therefor is run after each test.
        /// </summary>
        [TestCleanup]
        public virtual void TestCleanup()
        {
            MockRepository.VerifyAll();
        }
    }
}
