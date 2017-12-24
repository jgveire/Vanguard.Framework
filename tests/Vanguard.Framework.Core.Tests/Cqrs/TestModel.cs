namespace Vanguard.Framework.Core.Tests.Cqrs
{
    using System;

    public class TestModel
    {
        public TestModel()
        {
        }

        public TestModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
