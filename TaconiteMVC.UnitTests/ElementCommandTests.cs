using System;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TaconiteMvc;

namespace TaconiteMVC.Tests
{
  [TestFixture]
  public class ElementCommandTests
  {
    #region CreateCommandXElement(ActionResultExecutor)

    [Test]
    public void CreateCommandXElement_RawHtml_ReturnsCorrectXElement()
    {
      var command = "command";
      var selector = "#selector";
      var html = "<div>Some HTML!</div>";
      var elementCommand = new ElementCommand(command, selector, html);
      var controllerContext = Substitute.For<ControllerContext>();
      var actionResultExecutor = Substitute.For<ActionResultExecutor>(controllerContext);

      var result = elementCommand.CreateCommandXElement(actionResultExecutor);

      result.Name.Should().Be((XName)command);
      result.Should().HaveAttribute("select", selector);
      result.FirstNode.NodeType.Should().Be(XmlNodeType.CDATA);
      result.Value.Should().Be(html);
    }

    [Test]
    public void CreateCommandXElement_PartialViewResult_ExecutesActionResultAndReturnsCorrectXElement()
    {
      var command = "command";
      var selector = "#selector";
      var html = "<div>Some HTML!</div>";
      var controllerContext = Substitute.For<ControllerContext>();
      var partialViewResult = Substitute.For<PartialViewResult>();
      var actionResultExecutor = Substitute.For<ActionResultExecutor>(controllerContext);
      actionResultExecutor.Execute(partialViewResult).Returns(html);
      var elementCommand = new ElementCommand(command, selector, partialViewResult);

      var result = elementCommand.CreateCommandXElement(actionResultExecutor);

      result.Name.Should().Be((XName)command);
      result.Should().HaveAttribute("select", selector);
      result.FirstNode.NodeType.Should().Be(XmlNodeType.CDATA);
      result.Value.Should().Be(html);
    }

    [Test]
    public void CreateCommandXElement_NullActionResultExecutor_ThrowsArgumentNullException()
    {
      var elementCommand = new ElementCommand("command", "#selector", "<div>Some HTML!</div>");

      Action action = () => elementCommand.CreateCommandXElement(null);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Equals(object)

    [Test]
    public void Equals_OtherObjectIsNotAnElementCommand_ReturnsFalse()
    {
      var other = new object();
      var elementCommand = new ElementCommand("command", "#selector", "<div>Some HTML!</div>");

      var result = elementCommand.Equals(other);

      result.Should().BeFalse();
    }

    #endregion

    #region Equals(ElementCommand)

    [Test]
    public void Equals_OtherElementCommandIsNull_ReturnsFalse()
    {
      var elementCommand = new ElementCommand("command", "#selector", "<div>Some HTML!</div>");
      ElementCommand other = null;

      var result = elementCommand.Equals(other);

      result.Should().BeFalse();
    }

    [Test]
    public void Equals_OtherElementCommandIsSameObject_ReturnsTrue()
    {
      var elementCommand = new ElementCommand("command", "#selector", "<div>Some HTML!</div>");

      var result = elementCommand.Equals(elementCommand);

      result.Should().BeTrue();
    }

    [TestCase("command", "#selector", "html", "command", "#selector", "html")]
    [TestCase("command", "#selector", "html", "COMMAND", "#selector", "html")]
    public void Equals_Html_OtherElementCommandPropertiesSame_ReturnsTrue(
      string command0, string selector0, string html0,
      string command1, string selector1, string html1)
    {
      var elementCommand0 = new ElementCommand(command0, selector0, html0);
      var elementCommand1 = new ElementCommand(command1, selector1, html1);

      var result = elementCommand0.Equals(elementCommand1);

      result.Should().BeTrue();
    }

    [TestCase("command0", "#selector", "html", "command1", "#selector", "html")]
    [TestCase("command", "#selector", "html", "command", "#SELECTOR", "html")]
    [TestCase("command", "#selector0", "html", "command", "#selector1", "html")]
    [TestCase("command", "#selector", "html", "command", "#selector", "HTML")]
    [TestCase("command", "#selector", "html0", "command", "#selector", "html1")]
    public void Equals_Html_OtherElementCommandPropertiesDifferent_ReturnsFalse(
      string command0, string selector0, string html0,
      string command1, string selector1, string html1)
    {
      var elementCommand0 = new ElementCommand(command0, selector0, html0);
      var elementCommand1 = new ElementCommand(command1, selector1, html1);

      var result = elementCommand0.Equals(elementCommand1);

      result.Should().BeFalse();
    }

    [Test]
    public void Equals_PartialViewResult_OtherElementCommandPropertiesSame_ReturnsTrue()
    {
      var command = "command";
      var selector = "#selector";
      var partialViewResult = new PartialViewResult();
      var elementCommand0 = new ElementCommand(command, selector, partialViewResult);
      var elementCommand1 = new ElementCommand(command, selector, partialViewResult);

      var result = elementCommand0.Equals(elementCommand1);

      result.Should().BeTrue();
    }

    [Test]
    public void Equals_PartialViewResult_PartialViewResultsDifferent_ReturnsFalse()
    {
      var command = "command";
      var selector = "#selector";
      var elementCommand0 = new ElementCommand(command, selector, new PartialViewResult());
      var elementCommand1 = new ElementCommand(command, selector, new PartialViewResult());

      var result = elementCommand0.Equals(elementCommand1);

      result.Should().BeFalse();
    }

    [Test]
    public void Equals_OneHtmlAndOnePartialViewResult_ReturnsFalse()
    {
      var command = "command";
      var selector = "#selector";
      var elementCommand0 = new ElementCommand(command, selector, "<div>Some HTML!</div>");
      var elementCommand1 = new ElementCommand(command, selector, new PartialViewResult());

      var result = elementCommand0.Equals(elementCommand1);

      result.Should().BeFalse();
    }

    #endregion

    #region GetHashCode()

    [TestCase("command", "#selector", "html", "command", "#selector", "html")]
    [TestCase("command", "#selector", "html", "COMMAND", "#selector", "html")]
    public void GetHashCode_Html_ElementCommandsHaveSameProperties_ReturnsSameHashCode(
      string command0, string selector0, string html0,
      string command1, string selector1, string html1)
    {
      var elementCommand0 = new ElementCommand(command0, selector0, html0);
      var elementCommand1 = new ElementCommand(command1, selector1, html1);

      var result0 = elementCommand0.GetHashCode();
      var result1 = elementCommand1.GetHashCode();

      result0.Should().Be(result1);
    }

    [TestCase("command0", "#selector", "html", "command1", "#selector", "html")]
    [TestCase("command", "#selector", "html", "command", "#SELECTOR", "html")]
    [TestCase("command", "#selector0", "html", "command", "#selector1", "html")]
    [TestCase("command", "#selector", "html", "command", "#selector", "HTML")]
    [TestCase("command", "#selector", "html0", "command", "#selector", "html1")]
    public void GetHashCode_Html_ElementCommandsHaveDifferentProperties_ReturnsDifferentHashCode(
      string command0, string selector0, string html0,
      string command1, string selector1, string html1)
    {
      var elementCommand0 = new ElementCommand(command0, selector0, html0);
      var elementCommand1 = new ElementCommand(command1, selector1, html1);

      var result0 = elementCommand0.GetHashCode();
      var result1 = elementCommand1.GetHashCode();

      result0.Should().NotBe(result1);
    }

    [Test]
    public void GetHashCode_PartialViewResult_PartialViewResultsDifferent_ReturnsDifferentHashCode()
    {
      var command = "command";
      var selector = "#selector";
      var elementCommand0 = new ElementCommand(command, selector, new PartialViewResult());
      var elementCommand1 = new ElementCommand(command, selector, new PartialViewResult());

      var result0 = elementCommand0.GetHashCode();
      var result1 = elementCommand1.GetHashCode();

      result0.Should().NotBe(result1);
    }

    [Test]
    public void GetHashCode_OneHtmlAndOnePartialViewResult_ReturnsFalse()
    {
      var command = "command";
      var selector = "#selector";
      var elementCommand0 = new ElementCommand(command, selector, "<div>Some HTML!</div>");
      var elementCommand1 = new ElementCommand(command, selector, new PartialViewResult());

      var result0 = elementCommand0.GetHashCode();
      var result1 = elementCommand1.GetHashCode();

      result0.Should().NotBe(result1);
    }

    #endregion
  }
}
