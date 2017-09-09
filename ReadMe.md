# Vanguard Framework

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
This allows you to setup the mocking behavior of the type you want to mock. For more information about mock please look at the documentation of [Moq](https://github.com/moq/moq4).

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
