using System;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using NUnit.Framework;
using TaconiteMvc;

namespace TaconiteMVC.Tests.FluentSyntax
{
  [TestFixture]
  public class ExecutePluginSyntaxTests
  {
    #region Execute Plugin (base)

    [Test]
    public void ExecutePlugin_NullOrEmptyPluginName_ThrowsArgumentNullException(
      [Values(null, "")] string pluginName)
    {
      Action action = () => Taconite.ExecutePlugin(pluginName);

      action.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void ExecutePlugin_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      var plugin = "plugin";

      Action action = () => Taconite.ExecutePlugin(plugin).For(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Execute Plugin with PartialViewResult Content

    [Test]
    public void ExecutePluginWithPartialViewResultContent()
    {
      var plugin = "plugin";
      var selector = "#selector";
      var partialViewResult = new PartialViewResult();

      var result = Taconite.ExecutePlugin(plugin).For(selector).WithContent(partialViewResult);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = plugin,
            Html = (string)null,
            Selector = selector,
            PartialViewResult = partialViewResult
          });
    }

    [Test]
    public void ExecutePluginWithPartialViewResult_NullPartialViewResult_ThrowsArgumentNullException()
    {
      var plugin = "plugin";
      var selector = "#selector";

      Action action = () => Taconite.ExecutePlugin(plugin).For(selector).WithContent((PartialViewResult)null);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Execute Plugin with PartialView Content

    [Test]
    public void ExecutePluginWithPartialView_ViewNameAndModelNotSpecified()
    {
      var plugin = "plugin";
      var selector = "#selector";

      var result = Taconite.ExecutePlugin(plugin).For(selector).WithPartialView();

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = plugin,
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().BeNull();
      command.As<ElementCommand>().Partial.View.Should().BeNull();
    }

    [Test]
    public void ExecutePluginWithPartialView_ViewNameSpecifiedAndModelNotSpecified()
    {
      var plugin = "plugin";
      var selector = "#selector";
      var viewName = "View";

      var result = Taconite.ExecutePlugin(plugin).For(selector).WithPartialView(viewName);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = plugin,
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().BeNull();
      command.As<ElementCommand>().Partial.ViewName.Should().Be(viewName);
    }

    [Test]
    public void ExecutePluginWithPartialView_ViewNameNotSpecifiedAndModelSpecified()
    {
      var plugin = "plugin";
      var selector = "#selector";
      var model = new object();

      var result = Taconite.ExecutePlugin(plugin).For(selector).WithPartialView(model);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = plugin,
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().Be(model);
      command.As<ElementCommand>().Partial.View.Should().BeNull();
    }

    [Test]
    public void ExecutePluginWithPartialView_ViewNameAndModelSpecified()
    {
      var plugin = "plugin";
      var selector = "#selector";
      var viewName = "View";
      var model = new object();

      var result = Taconite.ExecutePlugin(plugin).For(selector).WithPartialView(viewName, model);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = plugin,
            Html = (string) null,
            Selector = selector
          });
      command.As<ElementCommand>().Partial.Model.Should().Be(model);
      command.As<ElementCommand>().Partial.ViewName.Should().Be(viewName);
    }

    #endregion

    #region Execute Plugin with Raw HTML Content

    [Test]
    public void ExecutePluginWithRawHtmlContent()
    {
      var plugin = "plugin";
      var selector = "#selector";
      var html = "<div>Some HTML!</div>";

      var result = Taconite.ExecutePlugin(plugin).For(selector).WithContent(html);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<ElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = plugin,
            Html = html,
            Selector = selector,
            PartialViewResult = (PartialViewResult) null
          });
    }

    [Test]
    public void ExecutePluginWithRawHtmlContent_NullHtmlContent_ThrowsArgumentNullException()
    {
      var plugin = "plugin";
      var selector = "#selector";

      Action action = () => Taconite.ExecutePlugin(plugin).For(selector).WithContent((string) null);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Execute Plugin with Arguments

    [Test]
    public void ExecutePluginWithArguments()
    {
      var plugin = "plugin";
      var selector = "#selector";
      var arg0 = new object();
      var arg1 = new object();

      var result = Taconite.ExecutePlugin(plugin).For(selector).WithArguments(arg0, arg1);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<NonElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = plugin,
            Selector = selector,
          });
      command.As<NonElementCommand>().Arguments.Should().ContainInOrder(arg0, arg1);
    }

    [Test]
    public void ExecutePluginWithArguments_NoArguments()
    {
      var plugin = "plugin";
      var selector = "#selector";

      var result = Taconite.ExecutePlugin(plugin).For(selector).WithArguments();

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<NonElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = plugin,
            Selector = selector,
          });
      command.As<NonElementCommand>().Arguments.Should().BeEmpty();
    }

    #endregion

    #region Execute Plugin Without Arguments

    [Test]
    public void ExecutePluginWithNoArguments()
    {
      var plugin = "plugin";
      var selector = "#selector";

      var result = Taconite.ExecutePlugin(plugin).For(selector).WithNoArguments();

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<NonElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = plugin,
            Selector = selector,
          });
      command.As<NonElementCommand>().Arguments.Should().BeEmpty();
    }

    #endregion
  }
}
