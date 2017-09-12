namespace ExampleService.Controllers
{
    using System;
    using System.Linq;
    using ExampleData;
    using ExampleData.Entities;
    using ExampleService.Models;
    using Microsoft.AspNetCore.Mvc;
    using Vanguard.Framework.Core.Cqrs;
    using Vanguard.Framework.Http;

    [Route("api/cars")]
    public class CarController : CrudController<Guid, CarModel>
    {
        private readonly ExampleContext _context;

        public CarController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, ExampleContext context)
            : base(commandDispatcher, queryDispatcher)
        {
            _context = context;
            InitContext();
        }

        private void InitContext()
        {
            if (!_context.Cars.Any())
            {
                _context.Cars.Add(new Car(new Guid("9f490218-7db0-452f-8809-a390f9ca5a95"), "BMW", "Z5", "14-ZXD-5"));
                _context.Cars.Add(new Car(new Guid("5b2ca291-9fd6-4b36-abaf-4af27a64f322"), "Audi", "A4", "63-KDI-8"));
                _context.Cars.Add(new Car(new Guid("e8289985-aeaf-4867-85ab-af354c0d1d85"), "Audi", "A5", "03-UWE-3"));
                _context.Cars.Add(new Car(new Guid("fa47ff63-f5b7-4697-beae-6bb8384fd5b2"), "Opel", "Astra", "92-WUD-2"));
                _context.SaveChanges();
            }
        }
    }
}
