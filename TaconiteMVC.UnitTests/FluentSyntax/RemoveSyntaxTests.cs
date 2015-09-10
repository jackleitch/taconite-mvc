using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TaconiteMvc;

namespace TaconiteMVC.Tests.FluentSyntax
{
  [TestFixture]
  public class RemoveSyntaxTests
  {
    #region Remove Element(s)

    [Test]
    public void RemoveElement()
    {
      var selector = "#selector";

      var result = Taconite.Remove(selector);

      result.Commands.Should().HaveCount(1);
      var command = result.Commands.Single();
      command.As<NonElementCommand>()
        .Should().NotBeNull()
        .ShouldHave().SharedProperties().EqualTo(new
          {
            Command = "remove",
            Selector = selector
          });
      command.As<NonElementCommand>().Arguments.Should().BeEmpty();
    }

    [Test]
    public void RemoveElement_NullOrEmptyTargetSelector_ThrowsArgumentNullException(
      [Values(null, "")] string selector)
    {
      Action action = () => Taconite.Remove(selector);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion
  }
}
