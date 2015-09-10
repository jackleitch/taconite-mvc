using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TaconiteMvc;

namespace TaconiteMVC.Tests.FluentSyntax
{
  [TestFixture]
  public class AddClassSyntaxTests
  {
    #region Add Class to Element(s)

    [Test]
    public void AddClassToElement()
    {
      var className = "class";
      var selector = "#selector";

      var result = Taconite.AddClass(className).To(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<NonElementCommand>()
        .Should().NotBeNull()
        .And.ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "addClass",
            Selector = selector
          });
      command.As<NonElementCommand>().Arguments.Should().HaveCount(1)
        .And.Contain(className);
    }

    [Test]
    public void AddClassToElement_NullOrEmptyClassName_ThrowsArgumentNullException(
      [Values(null, "")] string className)
    {
      Action action = () => Taconite.AddClass(className);

      action.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void AddClassToElement_NullOrEmptySelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      var className = "class";

      Action action = () => Taconite.AddClass(className).To(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Add Classes to Element(s)

    [Test]
    public void AddClassesToElement_SingleClass()
    {
      var className = "class";
      var selector = "#selector";

      var result = Taconite.AddClasses(className).To(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<NonElementCommand>()
        .Should().NotBeNull()
        .And.ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "addClass",
            Selector = selector
          });
      command.As<NonElementCommand>().Arguments.Should().HaveCount(1)
        .And.Contain(className);
    }

    [Test]
    public void AddClassesToElement_TwoClasses()
    {
      var className0 = "class0";
      var className1 = "class1";
      var selector = "selector";

      var result = Taconite.AddClasses(className0, className1).To(selector);

      result.Commands.Should().HaveCount(1);

      var command = result.Commands.Single();
      command.As<NonElementCommand>()
        .Should().NotBeNull()
        .And.ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "addClass",
            Selector = selector
          });
      command.As<NonElementCommand>().Arguments.Should().HaveCount(1)
        .And.Contain(className0 + " " + className1);
    }

    [Test]
    public void AddClassesToElement_NoClassNames_ThrowsArgumentNullException()
    {
      var selector = "#selector";

      Action action = () => Taconite.AddClasses().To(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void AddClassesToElement_NullOrEmptyClassName_ThrowsArgumentNullException(
      [Values(null, "")] string className)
    {
      var selector = "#selector";

      Action action = () => Taconite.AddClasses(className).To(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void AddClassesToElement_NullOrEmptySelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      var className = "class";

      Action action = () => Taconite.AddClasses(className).To(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion
  }
}
