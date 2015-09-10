using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TaconiteMvc;

namespace TaconiteMVC.Tests.FluentSyntax
{
  [TestFixture]
  public class HideSyntaxTests
  {
    #region Hide Element(s)

    [Test]
    public void HideElement()
    {
      var selector = "#selector";

      var result = Taconite.Hide(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<NonElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "hide",
            Selector = selector
          });
      command.As<NonElementCommand>().Arguments.Should().BeEmpty();
    }

    [Test]
    public void HideElement_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      Action action = () => Taconite.Hide(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion
  }
}
