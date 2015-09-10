using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TaconiteMvc;

namespace TaconiteMVC.Tests.FluentSyntax
{
  [TestFixture]
  public class SetAttributeSyntaxTests
  {
    #region Set Attribute for Element(s)

    [Test]
    public void SetAttributeForElement()
    {
      var attributeName = "attribute";
      var attributeValue = 123;
      var selector = "#selector";

      var result = Taconite.SetAttribute(attributeName, attributeValue).For(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<NonElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "attr",
            Selector = selector
          });
      command.As<NonElementCommand>().Arguments.Should().ContainInOrder(new[] {attributeName, "123"});
    }

    [Test]
    public void SetAttributeForElement_NullOrEmptyAttributeName_ThrowsArgumentNullException(
      [Values(null, "")] string attributeName)
    {
      var attributeValue = 123;

      Action action = () => Taconite.SetAttribute(attributeName, attributeValue);

      action.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void SetAttributeForElement_NullAttributeValue_ThrowsArgumentNullException()
    {
      var attributeName = "attribute";

      Action action = () => Taconite.SetAttribute(attributeName, null);

      action.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void SetAttributeForElement_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      var attributeName = "attribute";
      var attributeValue = 123;

      Action action = () => Taconite.SetAttribute(attributeName, attributeValue).For(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Set Attributes for Element(s)

    [Test]
    public void SetAttributesForElement_ObjectHasOneProperty_ReturnsResultWithSingleCommand()
    {
      var attributes = new {attribute = 123};
      var selector = "#selector";

      var result = Taconite.SetAttributes(attributes).For(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<NonElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "attr",
            Selector = selector
          });
      command.As<NonElementCommand>().Arguments.Should().ContainInOrder(new[] {"attribute", "123"});
    }

    [Test]
    public void SetAttributesForElement_ObjectHasTwoProperties_ReturnsResultWithTwoCommands()
    {
      var attributes = new { attribute0 = 123, attribute1 = "value" };
      var selector = "#selector";

      var result = Taconite.SetAttributes(attributes).For(selector);

      foreach (var command in result.Commands)
      {
        command.As<NonElementCommand>()
          .Should().NotBeNull()
          .ShouldHave().SharedProperties().EqualTo(new
            {
              Command = "attr",
              Selector = selector
            });
      }
      result.Commands.ElementAt(0).As<NonElementCommand>().Arguments
        .Should().ContainInOrder(new[] { "attribute0", "123" });
      result.Commands.ElementAt(1).As<NonElementCommand>().Arguments
        .Should().ContainInOrder(new[] { "attribute1", "value" });
    }
    
    [Test]
    public void SetAttributesForElement_ObjectPropertyNameHasUnderscore_UnderscoreConvertedToHyphen()
    {
      var attributes = new {attribute_0 = 123};
      var selector = "#selector";

      var result = Taconite.SetAttributes(attributes).For(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<NonElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "attr",
            Selector = selector
          });
      command.As<NonElementCommand>().Arguments.Should().ContainInOrder(new[] { "attribute-0", "123" });
    }

    [Test]
    public void SetAttributesForElement_NullObject_ThrowsArgumentNullException()
    {
      Action action = () => Taconite.SetAttributes(null);

      action.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void SetAttributesForElement_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      var attributes = new {attribute = 123};

      Action action = () => Taconite.SetAttributes(attributes).For(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void SetAttributesForElement_ObjectHasTwoPropertiesWithSameNamesButDifferentCase_ThrowsArgumentException()
    {
      var attributes = new {ATTRIBUTE = 123, attribute = "value"};

      Action action = () => Taconite.SetAttributes(attributes);

      action.ShouldThrow<ArgumentException>();
    }

    [Test]
    public void SetAttributesForElement_ObjectHasNoProperties_ThrowsArgumentException()
    {
      var attributes = new {};

      Action action = () => Taconite.SetAttributes(attributes);

      action.ShouldThrow<ArgumentException>();
    }

    #endregion
  }
}
