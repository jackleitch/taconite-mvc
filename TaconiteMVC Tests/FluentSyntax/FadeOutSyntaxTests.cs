using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TaconiteMvc;

namespace TaconiteMVC.Tests.FluentSyntax
{
  [TestFixture]
  public class FadeOutSyntaxTests
  {
    #region Fade Out Element(s)

    [Test]
    public void FadeOutElement()
    {
      var selector = "#selector";

      var result = Taconite.FadeOut(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<NonElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "fadeOut",
            Selector = selector
          });
      command.As<NonElementCommand>().Arguments.Should().BeEmpty();
    }

    [Test]
    public void FadeInElement_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      Action action = () => Taconite.FadeOut(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion
  }
}
