using System;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using NUnit.Framework;
using TaconiteMvc;

namespace TaconiteMVC.Tests.FluentSyntax
{
  [TestFixture]
  public class PrependSyntaxTests
  {
    #region Prepend PartialViewResult Content to Element(s)

    [Test]
    public void PrependPartialViewResultContentToElement()
    {
      var partialViewResult = new PartialViewResult();
      var selector = "#selector";

      var result = Taconite.PrependContent(partialViewResult).To(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "prepend",
            Html = (string) null,
            Partial = partialViewResult,
            Selector = selector
          });
    }

    [Test]
    public void PrependPartialViewResultContentToElement_NullPartialViewResult_ThrowsArgumentNullException()
    {
      var selector = "#selector";

      Action action = () => Taconite.PrependContent((PartialViewResult) null).To(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void PrependPartialViewResultContentToElement_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      var partialViewResult = new PartialViewResult();

      Action action = () => Taconite.PrependContent(partialViewResult).To(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Prepend PartialView to Element(s)

    [Test]
    public void PrependPartialViewToElement_ViewNameAndModelNotSpecified()
    {
      var selector = "#selector";

      var result = Taconite.PrependPartialView().To(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "prepend",
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().BeNull();
      command.As<ElementCommand>().Partial.View.Should().BeNull();
    }

    [Test]
    public void PrependPartialViewToElement_ViewNameSpecifiedAndModelNotSpecified()
    {
      var viewName = "View";
      var selector = "#selector";

      var result = Taconite.PrependPartialView(viewName).To(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
        {
          Command = "prepend",
          Html = (string)null,
          Selector = selector
        });
      command.As<ElementCommand>().Partial.Model.Should().BeNull();
      command.As<ElementCommand>().Partial.ViewName.Should().Be(viewName);
    }

    [Test]
    public void PrependPartialViewToElement_ViewNameNotSpecifiedAndModelSpecified()
    {
      var model = new object();
      var selector = "#selector";

      var result = Taconite.PrependPartialView(model).To(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
        {
          Command = "prepend",
          Html = (string)null,
          Selector = selector
        });
      command.As<ElementCommand>().Partial.Model.Should().Be(model);
      command.As<ElementCommand>().Partial.View.Should().BeNull();
    }

    [Test]
    public void PrependPartialViewToElement_ViewNameAndModelSpecified()
    {
      var viewName = "View";
      var model = new object();
      var selector = "#selector";

      var result = Taconite.PrependPartialView(viewName, model).To(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
        {
          Command = "prepend",
          Html = (string)null,
          Selector = selector
        });
      command.As<ElementCommand>().Partial.Model.Should().Be(model);
      command.As<ElementCommand>().Partial.ViewName.Should().Be(viewName);
    }

    [Test]
    public void PrependPartialViewToElement_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      Action action = () => Taconite.PrependPartialView().To(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Prepend Raw HTML Content to Element(s)
    
    [Test]
    public void PrependRawHtmlContentToElement()
    {
      var selector = "#selector";
      var html = "<div>Some HTML!</div>";

      var result = Taconite.PrependContent(html).To(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "prepend",
            Html = html,
            PartialViewResult = (PartialViewResult)null,
            Selector = selector
          });
    }

    [Test]
    public void PrependRawHtmlContentToElement_NullHtmlContent_ThrowsArgumentNullException()
    {
      var selector = "#selector";

      Action action = () => Taconite.PrependContent((string) null).To(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void PrependRawHtmlContentToElement_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      var html = "<div>Some HTML!</div>";

      Action action = () => Taconite.PrependContent(html).To(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion
  }
}
