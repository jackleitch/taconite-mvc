using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TaconiteMvc;

namespace TaconiteMVC.Tests.FluentSyntax
{
  [TestFixture]
  public class RemoveAttributeSyntaxTests
  {
    #region Remove Attribute from Element(s)

    [Test]
    public void RemoveAttributeFromElement()
    {
      var attributeName = "attribute";
      var selector = "#selector";

      var result = Taconite.RemoveAttribute(attributeName).From(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<NonElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "removeAttr",
            Selector = selector
          });
      command.As<NonElementCommand>().Arguments.Should().HaveCount(1)
        .And.Contain(attributeName);
    }

    [Test]
    public void RemoveAttributeFromElement_NullOrEmptyAttributeName_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      Action action = () => Taconite.RemoveAttribute(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void RemoveAttributeFromElement_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      var attributeName = "attribute";

      Action action = () => Taconite.RemoveAttribute(attributeName).From(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Remove Attributes from Element(s)

    [Test]
    public void RemoveAttributesFromElement_SingleAttribute()
    {
      var attributeName = "attribute";
      var selector = "#selector";

      var result = Taconite.RemoveAttributes(attributeName).From(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<NonElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "removeAttr",
            Selector = selector
          });
      command.As<NonElementCommand>().Arguments.Should().HaveCount(1)
        .And.Contain(attributeName);
    }

    [Test]
    public void RemoveAttributesFromElement_TwoAttributes()
    {
      var attribute0 = "attribute0";
      var attribute1 = "attribute1";
      var selector = "#selector";

      var result = Taconite.RemoveAttributes(attribute0, attribute1).From(selector);

      result.Commands.Should().HaveCount(2);
      foreach (var command in result.Commands)
      {
        command.As<NonElementCommand>()
          .Should().NotBeNull()
          .ShouldHave().SharedProperties().EqualTo(new
            {
              Command = "removeAttr",
              Selector = selector
            });
      }
      result.Commands.ElementAt(0).As<NonElementCommand>().Arguments.Should().HaveCount(1)
        .And.Contain(attribute0);
      result.Commands.ElementAt(1).As<NonElementCommand>().Arguments.Should().HaveCount(1)
        .And.Contain(attribute1);
    }

    [Test]
    public void RemoveAttributesFromElement_NoAttributes_ThrowsArgumentNullException()
    {
      var selector = "#selector";

      Action action = () => Taconite.RemoveAttributes().From(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void RemoveAttributesFromElement_NullOrEmptyAttribute_ThrowsArgumentNullException(
      [Values(null, "")] string attribute)
    {
      var selector = "#selector";

      Action action = () => Taconite.RemoveAttributes(attribute).From(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void RemoveAttributesFromElement_NullOrEmptySelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      var attributeName = "attribute";

      Action action = () => Taconite.RemoveAttributes(attributeName).From(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion
  }
}
