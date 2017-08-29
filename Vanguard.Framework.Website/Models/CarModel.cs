using System;
using Vanguard.Framework.Core.Repositories;

namespace Vanguard.Framework.Website.Models
{
    public class CarModel : IUniqueEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Brand { get; set; }

        public string Model { get; set; }
    }
}
