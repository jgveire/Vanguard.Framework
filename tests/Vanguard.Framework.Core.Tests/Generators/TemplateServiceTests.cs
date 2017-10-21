using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vanguard.Framework.Core.Generators;
using Vanguard.Framework.Test;

namespace Vanguard.Framework.Core.Tests.Generators
{
    [TestClass]
    public class TemplateServiceTests : TestBase<SimpleDocumentGenerator>
    {
        public const string Layout = "<html><head></head><body>{{Content}}</body></html>";
        public const string Template = "The car brand is: {{brand}}, and model is {{Model}}.";
        private CarModel _templateModel = new CarModel("Volkswagen", "Golf", new[] { "Parking sensors" });

        [TestCleanup]
        public override void TestCleanup()
        {
            base.TestCleanup();
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

        [TestMethod]
        public void When_contructor_is_called_then_exception_should_be_thrown()
        {
            // Act
            Action action = () => new SimpleDocumentGenerator("Test Template");

            // Assert
            action.ShouldThrow<ArgumentException>(because: "a layout should contain the tag {{content}}");
        }

        [TestMethod]
        public void When_contructor_is_called_then_no_exception_should_be_thrown()
        {
            // Act
            Action action = () => new SimpleDocumentGenerator("{{content}}");

            // Assert
            action.ShouldNotThrow(because: "the layout contains the tag {{content}}");
        }

        [TestMethod]
        public void When_Generate_is_called_then_layout_should_be_used()
        {
            // Arrange
            var systemUnderTest = new SimpleDocumentGenerator(Layout);

            // Act
            var result = systemUnderTest.Generate("Hello {{Brand}}!", _templateModel);

            // Assert
            var expected = "<html><head></head><body>Hello Volkswagen!</body></html>";
            result.Should().Be(expected, because: "we expected the layout and template to be combined");
        }

        [TestMethod]
        public void When_Generate_is_called_then_options_should_be_converted_to_string()
        {
            // Act
            var result = SystemUnderTest.Generate("{{Options}}", _templateModel);

            // Assert
            var expected = "System.String[]";
            result.Should().Be(expected, because: "we expected the template tags to be replaced with the model values");
        }

        [TestMethod]
        public void When_Generate_is_called_then_tags_should_be_replaced()
        {
            // Act
            var result = SystemUnderTest.Generate(Template, _templateModel);

            // Assert
            var expected = "The car brand is: Volkswagen, and model is Golf.";
            result.Should().Be(expected, because: "we expected the template tags to be replaced with the model values");
        }

        [TestMethod]
        public void When_Generate_is_called_then_values_should_be_encoded()
        {
            // Arrange
            var systemUnderTest = new SimpleDocumentGenerator(DocumentType.Html);
            var model = new CarModel("<b>Volkswagen</b>");

            // Act
            var result = systemUnderTest.Generate("{{Brand}}", model);

            // Assert
            result.Should().Be("&lt;b&gt;Volkswagen&lt;/b&gt;", because: "we expected the html tags to be encoded");
        }
    }
}
