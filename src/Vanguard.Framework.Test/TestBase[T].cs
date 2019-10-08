namespace Vanguard.Framework.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// The test base class.
    /// </summary>
    /// <typeparam name="TSystemUnderTest">The type of the system under test.</typeparam>
    /// <seealso cref="Vanguard.Framework.Test.TestBase" />
    [TestClass]
    public class TestBase<TSystemUnderTest> : TestBase
        where TSystemUnderTest : class
    {
        private readonly Dictionary<Type, Mock> _mockDictionary = new Dictionary<Type, Mock>();
        private TSystemUnderTest? _systemUnderTest;

        /// <summary>
        /// Gets the system under test.
        /// </summary>
        /// <value>
        /// The system under test.
        /// </value>
        protected TSystemUnderTest SystemUnderTest => GetSystemUnderTest();

        /// <summary>
        /// Initializes each test and therefor is run before each test.
        /// </summary>
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            _systemUnderTest = default;
            _mockDictionary.Clear();
        }

        /// <summary>
        /// Cleans up after each test and therefor is run after each test.
        /// </summary>
        [TestCleanup]
        public override void TestCleanup()
        {
            base.TestCleanup();
        }

        /// <summary>
        /// Mocks the specified type.
        /// </summary>
        /// <typeparam name="T">The type that has to be mocked.</typeparam>
        /// <returns>A mock object for the specified type.</returns>
        protected Mock<T> Mocks<T>()
            where T : class
        {
            Type type = typeof(T);
            if (!_mockDictionary.ContainsKey(type))
            {
                Mock<T> mock = MockRepository.Create<T>();
                _mockDictionary.Add(type, mock);
            }

            return (Mock<T>)_mockDictionary[type];
        }

        /// <summary>
        /// Creates the system under test.
        /// </summary>
        /// <returns>The system under test.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the system under test type isn't a class, is an abastract class or the class has no constructors.
        /// </exception>
        protected virtual TSystemUnderTest CreateSystemUnderTest()
        {
            Type type = typeof(TSystemUnderTest);
            if (!type.IsClass || type.IsAbstract)
            {
                string message = string.Format(ExceptionResource.SystemUnderTestInvalidType, type.Name);
                throw new InvalidOperationException(message);
            }

            ConstructorInfo? constructor = type
                .GetConstructors(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance)
                .OrderBy(x => x.GetParameters().Count())
                .FirstOrDefault();
            if (constructor == null)
            {
                string message = string.Format(ExceptionResource.SystemUnderTestHasNoConstructor, type.Name);
                throw new InvalidOperationException(message);
            }

            var parameters = GetParameters(constructor.GetParameters());
            var result = constructor.Invoke(parameters);
            return (TSystemUnderTest)result;
        }

        private TSystemUnderTest GetSystemUnderTest()
        {
            if (_systemUnderTest == null)
            {
                _systemUnderTest = CreateSystemUnderTest();
            }

            return _systemUnderTest;
        }

        private object?[] GetParameters(ParameterInfo[] parameters)
        {
            var result = new List<object?>();

            foreach (var parameter in parameters)
            {
                Type type = parameter.ParameterType;
                if (_mockDictionary.ContainsKey(type))
                {
                    result.Add(_mockDictionary[type].Object);
                }
                else
                {
                    result.Add(null);
                }
            }

            return result.ToArray();
        }
    }
}
