using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TaconiteMvc;

namespace TaconiteMVC.Tests.FluentSyntax
{
  [TestFixture]
  public class FadeInSyntaxTests
  {
    #region Fade In Element(s)

    [Test]
    public void FadeInElement()
    {
      var selector = "#selector";

      var result = Taconite.FadeIn(selector);

      result.Commands.Should().HaveCount(1);

      var command = result.Commands.Single();
      command.As<NonElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "fadeIn",
            Selector = selector
          });
      command.As<NonElementCommand>().Arguments.Should().BeEmpty();
    }

    [Test]
    public void FadeInElement_NullOrEmptySelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      Action action = () => Taconite.FadeIn(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion
  }
}
