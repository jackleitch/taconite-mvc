using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TaconiteMvc;

namespace TaconiteMVC.Tests.FluentSyntax
{
  [TestFixture]
  public class ShowSyntaxTests
  {
    #region Show Element(s)

    [Test]
    public void ShowElement()
    {
      var selector = "#selector";

      var result = Taconite.Show(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<NonElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "show",
            Selector = selector
          });
      command.As<NonElementCommand>().Arguments.Should().BeEmpty();
    }

    [Test]
    public void ShowElement_NullOrEmptySelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      Action action = () => Taconite.Show(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion
  }
}
