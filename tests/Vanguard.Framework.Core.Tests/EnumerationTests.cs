namespace Vanguard.Framework.Core.Tests
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EnumerationTests
    {
        [TestMethod]
        public void When_FromValue_is_called_then_the_correct_Truck_should_be_returned()
        {
            // Act
            CarType result = Enumeration.FromValue<CarType>(1);

            // Assert
            result.Should().Be(CarType.Truck, because: "the car type truck has the value 1");
        }

        [TestMethod]
        public void When_FromDisplayName_is_called_then_the_correct_Truck_should_be_returned()
        {
            // Act
            CarType result = Enumeration.FromDisplayName<CarType>("Truck");

            // Assert
            result.Should().Be(CarType.Truck, because: "the car type truck has the display name 'Truck'");
        }

        private class CarType : Enumeration
        {
            public static readonly CarType Sedan = new CarType(0, "Sedan");

            public static readonly CarType Truck = new CarType(1, "Truck");

            public CarType(int value, string displayName)
                : base(value, displayName)
            {
            }
        }
    }
}
