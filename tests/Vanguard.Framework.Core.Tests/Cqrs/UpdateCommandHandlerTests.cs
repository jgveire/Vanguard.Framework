namespace Vanguard.Framework.Core.Tests.Cqrs
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Vanguard.Framework.Core.Cqrs;
    using Vanguard.Framework.Core.Repositories;
    using Vanguard.Framework.Test;

    [TestClass]
    public class UpdateCommandHandlerTests : TestBase<UpdateCommandHandler<TestModel, TestEntity>>
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            base.TestCleanup();
        }

        [TestMethod]
        public void When_Execute_is_called_the_model_should_be_mapped_to_the_entity()
        {
            // Arrange
            var id = new Guid("{34755600-CEC7-469C-8437-966DC174D3A8}");
            var model = new TestModel(id, "Test");
            var entity = new TestEntity(id, null);
            var command = new UpdateCommand<TestModel>(id, model);

            // Arrange mocks
            Mocks<IRepository<TestEntity>>()
                .Setup(repository => repository.GetById(id))
                .Returns(entity);
            Mocks<IRepository<TestEntity>>()
                .Setup(repository => repository.Update(entity));
            Mocks<IRepository<TestEntity>>()
                .Setup(repository => repository.Save())
                .Returns(1);
            Mocks<IMapper>()
                .Setup(mapper => mapper.Map(model, entity))
                .Returns(entity);

            // Act
            SystemUnderTest.Execute(command);

            // Assert
        }
    }
}
