# Vanguard Framework
# What
The Vanguard Framework is a framework for developing database driven web applications and web services. It combines a set of design patterns and best practices to kick start your project.

By using the Vanguard Framework and following its design patterns you ensure that your code is of good quality, can be maintained easily and extended without a lot of hassle.

The Vanguard Framework is based on the following design patterns.

1. Domain Driven Design (DDD)
2. Domain Events
3. Repository Pattern
4. Command Query Responsibility Segregation (CQRS)
5. Inversion of Control (IoC)

## Architecture
The Vanguard Framework is built with a n-tier architecture in mind. The Vanguard Framework takes the following layers into account.

1. Data Access Layer
2. Business Layer
3. Service Layer
4. User Interface Layer

### Data Access Layer
The data access layer is based on the entity framework. It is possible to build it around another framework but this would mean that you have to do some customization.

In the business layer the data/domain entities are located. The data entities can be accessed via repositories and the data entities can raise Domain Events.

### Business Layer
The business layer contains all business logic like commands, queries, validator and event handlers.

### Service Layer
The service layer is based on ASP.NET Web API 2.0 and make functionality available via JSON REST service. The service layer consists of little to no functionality. It passes all request to the business layer. The reason for this is that we want the application logic reside in one location, the business layer. It also makes it easier to change the service layer framework to something else like Windows Communication Foundation (WCF). 

### User Interface Layer
The framework for the user interface layer has not been identified yet because the Vanguard Framework is work in progress. As soon as the functionality for the other layers are finished we will decide on this. For now we are leaning towards AngularJS 4.0 but this is not set in stone.

## Vanguard Test Framework
The Vanguard Test Framework is based on the [Microsoft Test Framework](https://github.com/microsoft/testfx) and [Moq](https://github.com/moq/moq4) (for mocking classes and interface). 
It adds some base test classes to make it easier to write unit tests.
The TestBase class handles the creation of the system under test, or in other words the class that is going to be tested. 
It looks for the constructor with the most arguments and creates it. If you look at the example below you see that the simple class does not have a constructor.
In that scenario, it will use the default constructor.

```csharp
public class Simple
{
    public int Multiply(int a, int b)
    {
        return a * b;
    }
}

[TestClass]
public class SimpleTests : TestBase<Simple>
{
    [TestMethod]
    public void When_Multiply_Is_Called_Then_Result_Should_Be_Twenty()
    {
        // Arrange
        int a = 2;
        int b = 10;

        // Act
        int result = SystemUnderTest.Multiply(a, b);

        // Assert
        Assert.AreEqual(20, result, "the result should be 20 because 2 times 10 is 20");
    }
}
```

When the system under test has a constructor with arguments you must ensure these arguments are mocked. You can do this by calling the ```Mocks<T>()``` method.
This allows you to setup the mocking behavior of the type you want to mock. For more information about mocking please look at the documentation of [Moq](https://github.com/moq/moq4).

```csharp
public interface IRepository
{
    object GetById(int id);
}

public class Complex
{
    private IRepository _repository;

    public Complex(IRepository repository)
    {
        _repository = repository;
    }

    public bool HasEntry(int id)
    {
        return _repository.GetById(id) != null;
    }
}

[TestClass]
public class ComplexTests : TestBase<Complex>
{
    [TestMethod]
    public void When_HasEntry_Is_Called_Then_Result_Should_Be_False()
    {
        // Arrange
        int Id = 2;

        // Arrange mocks
        Mocks<IRepository>().Setup(repository => repository.GetById(Id)).Returns(null);

        // Act
        bool result = SystemUnderTest.HasEntry(Id);

        // Assert
        Assert.IsFalse(result, "should be false because the repository get by identifier method returns null");
    }
}
```

If you don't have any setups/behavior for an argument just call the ```Mocks<T>()``` method without a setup. 

```csharp
Mocks<IRepository>();
```

If you don't want an argument to be mocked don't call the ```Mocks<T>()``` method for that type. 

If you want to create the system under test manually you can override the CreateSystemUnderTest method.
The default value will be used for that argument; for example an ```int``` will result in ```0``` and a ```string``` will result in ```null```.

```csharp
[TestClass]
public class ComplexTests : TestBase<Complex>
{
    protected override Complex CreateSystemUnderTest()
    {
        return new Complex(new Repository());
    }
}

```

### Advanced Topics
The following steps are executed when running a unit test inside the TestBase class:

1. Create a new/fresh mock repository.
2. Clear all mocks.
3. Execute test
   1. Arrange test
   2. Arrange mocks
   3. Create system under test
   4. Act
   5. Assert 
4. Verify all mock setups

You can enhance or change this behavior by overriding the test initialize, test cleanup and create system under test methods.

```csharp
[TestClass]
public class ComplexTests : TestBase<Complex>
{
    [TestInitialize]
    public override void TestInitialize()
    {
        base.TestInitialize();
        // Custom code
    }

    [TestCleanup]
    public override void TestCleanup()
    {
        base.TestCleanup();
        // Custom code
    }

    protected override Complex CreateSystemUnderTest()
    {
        // Custom code
    }
}
```
