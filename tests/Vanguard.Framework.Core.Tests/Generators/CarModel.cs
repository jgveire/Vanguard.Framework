namespace Vanguard.Framework.Core.Tests.Generators
{
    public class CarModel
    {
        public CarModel(string brand = null, string model = null, string[] options = null)
        {
            Brand = brand;
            Model = model;
            Options = options;
        }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string[] Options { get; set; }
    }
}