using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TaconiteMvc;

namespace TaconiteMVC.Tests.FluentSyntax
{
  [TestFixture]
  public class SlideDownSyntaxTests
  {
    #region Slide-Down Element(s)

    [Test]
    public void SlideDownElement()
    {
      var selector = "#selector";

      var result = Taconite.SlideDown(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<NonElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "slideDown",
            Selector = selector
          });
      command.As<NonElementCommand>().Arguments.Should().BeEmpty();
    }

    [Test]
    public void SlideDownElement_NullOrEmptySelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      Action action = () => Taconite.SlideDown(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion
  }
}
