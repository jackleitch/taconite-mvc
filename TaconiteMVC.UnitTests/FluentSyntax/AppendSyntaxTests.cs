using System;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using NUnit.Framework;
using TaconiteMvc;

namespace TaconiteMVC.Tests.FluentSyntax
{
  [TestFixture]
  public class AppendSyntaxTests
  {
    #region Append PartialViewResult Content to Element(s)

    [Test]
    public void AppendPartialViewResultContentToElement()
    {
      var partialViewResult = new PartialViewResult();
      var selector = "#selector";

      var result = Taconite.AppendContent(partialViewResult).To(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "append",
            Html = (string) null,
            Partial = partialViewResult,
            Selector = selector
          });
    }

    [Test]
    public void AppendPartialViewResultContentToElement_NullPartialViewResult_ThrowsArgumentNullException()
    {
      var selector = "#selector";

      Action action = () => Taconite.AppendContent((PartialViewResult) null).To(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void AppendPartialViewResultContentToElement_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      var partialViewResult = new PartialViewResult();

      Action action = () => Taconite.AppendContent(partialViewResult).To(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Append PartialView to Element(s)

    [Test]
    public void AppendPartialViewToElement_ViewNameAndModelNotSpecified()
    {
      var selector = "#selector";

      var result = Taconite.AppendPartialView().To(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "append",
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().BeNull();
      command.As<ElementCommand>().Partial.View.Should().BeNull();
    }

    [Test]
    public void AppendPartialViewToElement_ViewNameSpecifiedAndModelNotSpecified()
    {
      var viewName = "View";
      var selector = "#selector";

      var result = Taconite.AppendPartialView(viewName).To(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "append",
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().BeNull();
      command.As<ElementCommand>().Partial.ViewName.Should().Be(viewName);
    }

    [Test]
    public void AppendPartialViewToElement_ViewNameNotSpecifiedAndModelSpecified()
    {
      var model = new object();
      var selector = "#selector";

      var result = Taconite.AppendPartialView(model).To(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "append",
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().Be(model);
      command.As<ElementCommand>().Partial.View.Should().BeNull();
    }

    [Test]
    public void AppendPartialViewToElement_ViewNameAndModelSpecified()
    {
      var viewName = "View";
      var model = new object();
      var selector = "#selector";

      var result = Taconite.AppendPartialView(viewName, model).To(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "append",
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().Be(model);
      command.As<ElementCommand>().Partial.ViewName.Should().Be(viewName);
    }

    [Test]
    public void AppendPartialViewToElement_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      Action action = () => Taconite.AppendPartialView().To(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Append Raw HTML Content to Element(s)

    [Test]
    public void AppendRawHtmlContentToElement()
    {
      var selector = "#selector";
      var html = "<div>Some HTML!</div>";

      var result = Taconite.AppendContent(html).To(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "append",
            Html = html,
            PartialViewResult = (PartialViewResult)null,
            Selector = selector
          });
    }

    [Test]
    public void AppendRawHtmlContentToElement_NullHtmlContent_ThrowsArgumentNullException()
    {
      var selector = "#selector";

      Action action = () => Taconite.AppendContent((string) null).To(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void AppendRawHtmlContentToElement_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      var html = "<div>Some HTML!</div>";

      Action action = () => Taconite.AppendContent(html).To(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion
  }
}
