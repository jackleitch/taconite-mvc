using System;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using NUnit.Framework;
using TaconiteMvc;

namespace TaconiteMVC.Tests.FluentSyntax
{
  [TestFixture]
  public class ExecuteTests
  {
    #region Execute Raw JavaScript

    [Test]
    public void ExecuteRawJavaScript()
    {
      var script = "console.log('hi');";

      var result = Taconite.Execute(script);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<EvalCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Script = script,
            JavaScriptResult = (JavaScriptResult)null
          });
    }

    [Test]
    public void ExecuteRawJavaScript_NullOrEmptyScript_ThrowsArgumentNullException(
      [Values(null, "")] string script)
    {
      Action action = () => Taconite.Execute(script);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Execute JavaScriptResult

    [Test]
    public void ExecuteJavaScriptResult()
    {
      var javaScriptResult = new JavaScriptResult();

      var result = Taconite.Execute(javaScriptResult);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<EvalCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Script = (string) null,
            JavaScriptResult = javaScriptResult
          });
    }

    [Test]
    public void ExecuteJavaScriptResult_NullJavaScriptResult_ThrowsArgumentNullException()
    {
      Action action = () => Taconite.Execute((JavaScriptResult) null);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion
  }
}
