using System;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using NUnit.Framework;
using TaconiteMvc;

namespace TaconiteMVC.Tests.FluentSyntax
{
  [TestFixture]
  public class ReplaceSyntaxTests
  {
    #region Replace Element(s) (base)

    [Test]
    public void ReplaceElements_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      Action action = () => Taconite.Replace(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Replace Element(s) with PartialViewResult Content

    [Test]
    public void ReplaceElementsWithPartialViewResultContent()
    {
      var partialViewResult = new PartialViewResult();
      var selector = "#selector";

      var result = Taconite.Replace(selector).WithContent(partialViewResult);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "replace",
            Html = (string)null,
            Partial = partialViewResult,
            Selector = selector
          });
    }

    [Test]
    public void ReplaceElementsWithPartialViewResultContent_NullPartialViewResult_ThrowsArgumentNullException()
    {
      var selector = "#selector";

      Action action = () => Taconite.Replace(selector).WithContent((PartialViewResult) null);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Replace Element(s) with PartialView

    [Test]
    public void ReplaceElementsWithPartialView_ViewNameAndModelNotSpecified()
    {
      var selector = "#selector";

      var result = Taconite.Replace(selector).WithPartialView();

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "replace",
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().BeNull();
      command.As<ElementCommand>().Partial.View.Should().BeNull();
    }

    [Test]
    public void ReplaceElementsWithPartialView_ViewNameSpecifiedAndModelNotSpecified()
    {
      var viewName = "View";
      var selector = "#selector";

      var result = Taconite.Replace(selector).WithPartialView(viewName);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "replace",
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().BeNull();
      command.As<ElementCommand>().Partial.ViewName.Should().Be(viewName);
    }

    [Test]
    public void ReplaceElementsWithPartialView_ViewNameNotSpecifiedAndModelSpecified()
    {
      var model = new object();
      var selector = "#selector";

      var result = Taconite.Replace(selector).WithPartialView(model);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "replace",
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().Be(model);
      command.As<ElementCommand>().Partial.View.Should().BeNull();
    }

    [Test]
    public void ReplaceElementsWithPartialView_ViewNameAndModelSpecified()
    {
      var viewName = "View";
      var model = new object();
      var selector = "#selector";

      var result = Taconite.Replace(selector).WithPartialView(viewName, model);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "replace",
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().Be(model);
      command.As<ElementCommand>().Partial.ViewName.Should().Be(viewName);
    }

    #endregion

    #region Replace Element(s) with Raw HTML Content

    [Test]
    public void ReplaceElementsWithRawHtmlContent()
    {
      var selector = "#selector";
      var html = "<div>Some HTML!</div>";

      var result = Taconite.Replace(selector).WithContent(html);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "replace",
            Html = html,
            PartialViewResult = (PartialViewResult) null,
            Selector = selector
          });
    }

    [Test]
    public void ReplaceElementsWithRawHtmlContent_NullHtmlContent_ThrowsArgumentNullException()
    {
      var selector = "#selector";

      Action action = () => Taconite.Replace(selector).WithContent((string)null);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion
  }
}
