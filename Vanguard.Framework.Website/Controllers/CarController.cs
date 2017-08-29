using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Vanguard.Framework.Website.Contexts;
using Vanguard.Framework.Website.Entities;

namespace Vanguard.Framework.Website.Controllers
{
    [Route("api/cars")]
    public class CarController : Controller
    {
        private ExampleContext _context;

        public CarController(ExampleContext context)
        {
            _context = context;
            InitContext();
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            //if (model.Page.HasValue)
            //{
            //    Response.Headers.Add("X-Total-Count", _context.Cars.Count().ToString());
            //    return Json(_context.Cars.Skip(model.Page.Value - (1 * model.PageSize.GetValueOrDefault(20))).Take(model.PageSize.GetValueOrDefault(20)));
            //}

            return Json(_context.Cars);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Json(_context.Cars.Find(id));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody, Required]string brand, [Required]string model)
        {
            var car = new Car(brand, model);
            _context.Cars.Add(car);
            _context.SaveChanges();
            return CreatedAtRoute(nameof(Get), car.Id, car);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody, Required]string brand, [Required]string model)
        {
            var car = _context.Cars.Find(id);
            car.Brand = brand;
            car.Model = model;
            _context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var car = _context.Cars.Find(id);
            _context.Cars.Remove(car);
            _context.SaveChanges();
            return new NoContentResult();
        }

        private void InitContext()
        {
            if (_context.Cars.Count() == 0)
            {
                _context.Cars.Add(new Car("BMW", "Z5"));
                _context.Cars.Add(new Car("Audi", "A4"));
                _context.Cars.Add(new Car("Audi", "A5"));
                _context.Cars.Add(new Car("Opel", "Astra"));
                _context.SaveChanges();
            }
        }
    }
}
