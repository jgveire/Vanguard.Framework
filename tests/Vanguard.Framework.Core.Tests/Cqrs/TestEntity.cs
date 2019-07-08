namespace Vanguard.Framework.Core.Tests.Cqrs
{
    using System;
    using Vanguard.Framework.Core.Repositories;

    public class TestEntity : IUniqueEntity
    {
        public TestEntity()
        {
        }

        public TestEntity(Guid id, string? name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }

        public string? Name { get; set; }
    }
}
