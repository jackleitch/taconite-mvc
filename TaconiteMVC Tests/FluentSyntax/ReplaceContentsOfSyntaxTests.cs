using System;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using NUnit.Framework;
using TaconiteMvc;

namespace TaconiteMVC.Tests.FluentSyntax
{
  [TestFixture]
  public class ReplaceContentsOfContentsOfSyntaxTests
  {
    #region Replace Contents of Element(s) (base)

    [Test]
    public void ReplaceContentsOfElements_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      Action action = () => Taconite.ReplaceContentsOf(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Replace Contents of Element(s) with PartialViewResult Content

    [Test]
    public void ReplaceContentsOfElementsWithPartialViewResultContent()
    {
      var partialViewResult = new PartialViewResult();
      var selector = "#selector";

      var result = Taconite.ReplaceContentsOf(selector).WithContent(partialViewResult);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "replaceContent",
            Html = (string)null,
            Partial = partialViewResult,
            Selector = selector
          });
    }

    [Test]
    public void ReplaceContentsOfElementsWithPartialViewResultContent_NullPartialViewResult_ThrowsArgumentNullException()
    {
      var selector = "#selector";

      Action action = () => Taconite.ReplaceContentsOf(selector).WithContent((PartialViewResult) null);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Replace Contents of Element(s) with PartialView

    [Test]
    public void ReplaceContentsOfElementsWithPartialView_ViewNameAndModelNotSpecified()
    {
      var selector = "#selector";

      var result = Taconite.ReplaceContentsOf(selector).WithPartialView();

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "replaceContent",
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().BeNull();
      command.As<ElementCommand>().Partial.View.Should().BeNull();
    }

    [Test]
    public void ReplaceContentsOfElementsWithPartialView_ViewNameSpecifiedAndModelNotSpecified()
    {
      var viewName = "View";
      var selector = "#selector";

      var result = Taconite.ReplaceContentsOf(selector).WithPartialView(viewName);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "replaceContent",
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().BeNull();
      command.As<ElementCommand>().Partial.ViewName.Should().Be(viewName);
    }

    [Test]
    public void ReplaceContentsOfElementsWithPartialView_ViewNameNotSpecifiedAndModelSpecified()
    {
      var model = new object();
      var selector = "#selector";

      var result = Taconite.ReplaceContentsOf(selector).WithPartialView(model);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "replaceContent",
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().Be(model);
      command.As<ElementCommand>().Partial.View.Should().BeNull();
    }

    [Test]
    public void ReplaceContentsOfElementsWithPartialView_ViewNameAndModelSpecified()
    {
      var viewName = "View";
      var model = new object();
      var selector = "#selector";

      var result = Taconite.ReplaceContentsOf(selector).WithPartialView(viewName, model);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "replaceContent",
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().Be(model);
      command.As<ElementCommand>().Partial.ViewName.Should().Be(viewName);
    }

    #endregion

    #region Replace Contents of Element(s) with Raw HTML Content

    [Test]
    public void ReplaceContentsOfElementsWithRawHtmlContent()
    {
      var selector = "#selector";
      var html = "<div>Some HTML!</div>";

      var result = Taconite.ReplaceContentsOf(selector).WithContent(html);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "replaceContent",
            Html = html,
            PartialViewResult = (PartialViewResult) null,
            Selector = selector
          });
    }

    [Test]
    public void ReplaceContentsOfElementsWithRawHtmlContent_NullHtmlContent_ThrowsArgumentNullException()
    {
      var selector = "#selector";

      Action action = () => Taconite.ReplaceContentsOf(selector).WithContent((string)null);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion
  }
}
