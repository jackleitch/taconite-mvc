using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TaconiteMvc;

namespace TaconiteMVC.Tests.FluentSyntax
{
  [TestFixture]
  public class RemoveClassSyntaxTests
  {
    #region Remove Class from Element(s)

    [Test]
    public void RemoveClassFromElement()
    {
      var className = "class";
      var selector = "#selector";

      var result = Taconite.RemoveClass(className).From(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<NonElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "removeClass",
            Selector = selector
          });
      command.As<NonElementCommand>().Arguments.Should().HaveCount(1)
        .And.Contain(className);
    }

    [Test]
    public void RemoveClassFromElement_NullOrEmptyClassName_ThrowsArgumentNullException(
      [Values(null, "")] string className)
    {
      Action action = () => Taconite.RemoveClass(className);

      action.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void RemoveClassFromElement_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      var className = "class";

      Action action = () => Taconite.RemoveClass(className).From(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Remove Classes from Element(s)

    [Test]
    public void RemoveClassesFromElement_SingleClass()
    {
      var className = "class";
      var selector = "#selector";

      var result = Taconite.RemoveClasses(className).From(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<NonElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "removeClass",
            Selector = selector
          });
      command.As<NonElementCommand>().Arguments.Should().HaveCount(1)
        .And.Contain(className);
    }

    [Test]
    public void RemoveClassesFromElement_TwoClasses()
    {
      var class0 = "class0";
      var class1 = "class1";
      var selector = "#selector";

      var result = Taconite.RemoveClasses(class0, class1).From(selector);

      result.Commands.Should().HaveCount(2);
      foreach (var command in result.Commands)
      {
        command.As<NonElementCommand>()
          .Should().NotBeNull()
          .ShouldHave().SharedProperties().EqualTo(new
            {
              Command = "removeClass",
              Selector = selector
            });
      }
      result.Commands.ElementAt(0).As<NonElementCommand>().Arguments.Should().HaveCount(1)
        .And.Contain(class0);
      result.Commands.ElementAt(1).As<NonElementCommand>().Arguments.Should().HaveCount(1)
        .And.Contain(class1);
    }

    [Test]
    public void RemoveClassesFromElement_NoClasses_ThrowsArgumentNullException()
    {
      var selector = "#selector";

      Action action = () => Taconite.RemoveClasses().From(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void RemoveClassesFromElement_NullOrEmptyClass_ThrowsArgumentNullException(
      [Values(null, "")] string className)
    {
      var selector = "#selector";

      Action action = () => Taconite.RemoveClasses(className).From(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void RemoveClassesFromElement_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      var attributeName = "attribute";

      Action action = () => Taconite.RemoveClasses(attributeName).From(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion
  }
}
