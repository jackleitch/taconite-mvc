using System;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using NUnit.Framework;
using TaconiteMvc;

namespace TaconiteMVC.Tests.FluentSyntax
{
  [TestFixture]
  public class InsertSyntaxTests
  {
    #region Insert PartialViewResult Content (base)

    [Test]
    public void InsertPartialViewResultContent_NullPartialViewResult_ThrowsArgumentNullException()
    {
      Action action = () => Taconite.InsertContent((PartialViewResult)null);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Insert PartialViewResult Content After Element(s)

    [Test]
    public void InsertPartialViewResultContentAfterElement()
    {
      var partialViewResult = new PartialViewResult();
      var selector = "#selector";

      var result = Taconite.InsertContent(partialViewResult).After(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "after",
            Html = (string)null,
            Partial = partialViewResult,
            Selector = selector
          });
    }

    [Test]
    public void InsertPartialViewResultContentAfterElement_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      var partialViewResult = new PartialViewResult();

      Action action = () => Taconite.InsertContent(partialViewResult).After(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Insert PartialViewResult Content Before Element(s)

    [Test]
    public void InsertPartialViewResultContentBeforeElement()
    {
      var partialViewResult = new PartialViewResult();
      var selector = "#selector";

      var result = Taconite.InsertContent(partialViewResult).Before(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
        {
          Command = "before",
          Html = (string)null,
          Partial = partialViewResult,
          Selector = selector
        });
    }

    [Test]
    public void InsertPartialViewResultContentBeforeElement_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      var partialViewResult = new PartialViewResult();

      Action action = () => Taconite.InsertContent(partialViewResult).Before(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Insert Raw HTML Content (base)

    [Test]
    public void InsertRawHtmlContent_NullHtmlContent_ThrowsArgumentNullException()
    {
      Action action = () => Taconite.InsertContent((string)null);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Insert Raw HTML Content After Element(s)

    [Test]
    public void InsertRawHtmlContentAfterElement()
    {
      var html = "<div>Some HTML!</div>";
      var selector = "#selector";

      var result = Taconite.InsertContent(html).After(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "after",
            Html = html,
            Partial = (PartialViewResult) null,
            Selector = selector
          });
    }

    [Test]
    public void InsertRawHtmlContentAfterElement_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      var html = "<div>Some HTML!</div>";

      Action action = () => Taconite.InsertContent(html).After(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Insert Raw HTML Content Before Element(s)

    [Test]
    public void InsertRawHtmlContentBeforeElement()
    {
      var html = "<div>Some HTML!</div>";
      var selector = "#selector";

      var result = Taconite.InsertContent(html).Before(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
        {
          Command = "before",
          Html = html,
          Partial = (PartialViewResult)null,
          Selector = selector
        });
    }

    [Test]
    public void InsertRawHtmlContentBeforeElement_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      var html = "<div>Some HTML!</div>";

      Action action = () => Taconite.InsertContent(html).Before(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Insert PartialView After Element(s)
    
    [Test]
    public void InsertPartialViewAfterElement_ViewNameAndModelNotSpecified()
    {
      var selector = "#selector";

      var result = Taconite.InsertPartialView().After(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "after",
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().BeNull();
      command.As<ElementCommand>().Partial.View.Should().BeNull();
    }

    [Test]
    public void InsertPartialViewAfterElement_ViewNameSpecifiedAndModelNotSpecified()
    {
      var viewName = "View";
      var selector = "#selector";

      var result = Taconite.InsertPartialView(viewName).After(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "after",
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().BeNull();
      command.As<ElementCommand>().Partial.ViewName.Should().Be(viewName);
    }

    [Test]
    public void InsertPartialViewAfterElement_ViewNameNotSpecifiedAndModelSpecified()
    {
      var model = new object();
      var selector = "#selector";

      var result = Taconite.InsertPartialView(model).After(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "after",
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().Be(model);
      command.As<ElementCommand>().Partial.View.Should().BeNull();
    }

    public void InsertPartialViewAfterElement_ViewNameAndModelSpecified()
    {
      var viewName = "View";
      var model = new object();
      var selector = "#selector";

      var result = Taconite.InsertPartialView(viewName, model).After(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "after",
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().Be(model);
      command.As<ElementCommand>().Partial.ViewName.Should().Be(viewName);
    }

    [Test]
    public void InsertPartialViewAfterElement_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      Action action = () => Taconite.InsertPartialView().After(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion
    
    #region Insert PartialView Before Element(s)

    [Test]
    public void InsertPartialViewBeforeElement_ViewNameAndModelNotSpecified()
    {
      var selector = "#selector";

      var result = Taconite.InsertPartialView().Before(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
        {
          Command = "before",
          Html = (string)null,
          Selector = selector
        });
      command.As<ElementCommand>().Partial.Model.Should().BeNull();
      command.As<ElementCommand>().Partial.View.Should().BeNull();
    }

    [Test]
    public void InsertPartialViewBeforeElement_ViewNameSpecifiedAndModelNotSpecified()
    {
      var viewName = "View";
      var selector = "#selector";

      var result = Taconite.InsertPartialView(viewName).After(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
        {
          Command = "before",
          Html = (string)null,
          Selector = selector
        });
      command.As<ElementCommand>().Partial.Model.Should().BeNull();
      command.As<ElementCommand>().Partial.ViewName.Should().Be(viewName);
    }

    [Test]
    public void InsertPartialViewBeforeElement_ViewNameNotSpecifiedAndModelSpecified()
    {
      var model = new object();
      var selector = "#selector";

      var result = Taconite.InsertPartialView(model).After(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
        {
          Command = "before",
          Html = (string)null,
          Selector = selector
        });
      command.As<ElementCommand>().Partial.Model.Should().Be(model);
      command.As<ElementCommand>().Partial.View.Should().BeNull();
    }

    public void InsertPartialViewBeforeElement_ViewNameAndModelSpecified()
    {
      var viewName = "View";
      var model = new object();
      var selector = "#selector";

      var result = Taconite.InsertPartialView(viewName, model).After(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
        {
          Command = "before",
          Html = (string)null,
          Selector = selector
        });
      command.As<ElementCommand>().Partial.Model.Should().Be(model);
      command.As<ElementCommand>().Partial.ViewName.Should().Be(viewName);
    }

    [Test]
    public void InsertPartialViewBeforeElement_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      Action action = () => Taconite.InsertPartialView().Before(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion
  }
}
