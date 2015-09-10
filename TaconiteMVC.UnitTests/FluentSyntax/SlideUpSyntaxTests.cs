using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TaconiteMvc;

namespace TaconiteMVC.Tests.FluentSyntax
{
  [TestFixture]
  public class SlideUpSyntaxTests
  {
    #region Slide-Up Element(s)

    [Test]
    public void SlideUpElement()
    {
      var selector = "#selector";

      var result = Taconite.SlideUp(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<NonElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "slideUp",
            Selector = selector
          });
      command.As<NonElementCommand>().Arguments.Should().BeEmpty();
    }

    [Test]
    public void SlideUpElement_NullOrEmptySelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      Action action = () => Taconite.SlideUp(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion
  }
}
