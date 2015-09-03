using System;
using System.Web.Mvc;
using System.Xml.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TaconiteMvc;

namespace TaconiteMVC.Tests
{
  [TestFixture]
  public class NonElementCommandTests
  {
    #region CreateCommandXElement(ActionResultExecutor)

    [Test]
    public void CreateCommandXElement_NoArguments_ReturnsCorrectXElement()
    {
      var command = "command";
      var selector = "#selector";
      var nonElementCommand = new NonElementCommand(command, selector);
      var controllerContext = Substitute.For<ControllerContext>();
      var actionResultExecutor = Substitute.For<ActionResultExecutor>(controllerContext);

      var result = nonElementCommand.CreateCommandXElement(actionResultExecutor);

      result.Name.Should().Be((XName) command);
      result.Should().HaveAttribute("select", selector);
      result.Nodes().Should().BeEmpty();
    }

    [Test]
    public void CreateCommandXElement_SingleArgument_ReturnsCorrectXElement()
    {
      var command = "command";
      var selector = "#selector";
      var arg1 = "arg1";
      var nonElementCommand = new NonElementCommand(command, selector, arg1);
      var controllerContext = Substitute.For<ControllerContext>();
      var actionResultExecutor = Substitute.For<ActionResultExecutor>(controllerContext);

      var result = nonElementCommand.CreateCommandXElement(actionResultExecutor);

      result.Name.Should().Be((XName)command);
      result.Should().HaveAttribute("select", selector);
      result.Should().HaveAttribute("arg1", arg1);
      result.Nodes().Should().BeEmpty();
    }

    [Test]
    public void CreateCommandXElement_TwoArguments_ReturnsCorrectXElement()
    {
      var command = "command";
      var selector = "#selector";
      var arg1 = "arg1";
      var arg2 = 123;
      var nonElementCommand = new NonElementCommand(command, selector, arg1, arg2);
      var controllerContext = Substitute.For<ControllerContext>();
      var actionResultExecutor = Substitute.For<ActionResultExecutor>(controllerContext);

      var result = nonElementCommand.CreateCommandXElement(actionResultExecutor);

      result.Name.Should().Be((XName)command);
      result.Should().HaveAttribute("select", selector);
      result.Should().HaveAttribute("arg1", arg1);
      result.Should().HaveAttribute("arg2", "123");
      result.Nodes().Should().BeEmpty();
    }

    [Test]
    public void CreateCommandXElement_NullActionResultExecutor_ThrowsArgumentNullException()
    {
      var nonElementCommand = new NonElementCommand("command", "#selector");

      Action action = () => nonElementCommand.CreateCommandXElement(null);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Equals(object)

    [Test]
    public void Equals_OtherObjectIsNotANonElementCommand_ReturnsFalse()
    {
      var other = new object();
      var elementCommand = new NonElementCommand("command", "#selector");

      var result = elementCommand.Equals(other);

      result.Should().BeFalse();
    }

    #endregion

    #region Equals(NonElementCommand)

    [Test]
    public void Equals_OtherNonElementCommandIsNull_ReturnsFalse()
    {
      var nonElementCommand = new NonElementCommand("command", "#selector");
      ElementCommand other = null;

      var result = nonElementCommand.Equals(other);

      result.Should().BeFalse();
    }

    [Test]
    public void Equals_OtherNonElementCommandIsSameObject_ReturnsTrue()
    {
      var nonElementCommand = new NonElementCommand("command", "#selector");

      var result = nonElementCommand.Equals(nonElementCommand);

      result.Should().BeTrue();
    }

    [TestCase("command1", "#selector", new object[] { }, "command1", "#selector", new object[] { })]
    [TestCase("command2", "#selector", new object[] { }, "COMMAND2", "#selector", new object[] { })]
    [TestCase("command3", "#selector", new object[] { 1 }, "command3", "#selector", new object[] { 1 })]
    [TestCase("command4", "#selector", new object[] { null }, "command4", "#selector", new object[] { null })]
    [TestCase("command5", "#selector", new object[] { 1, "two" }, "command5", "#selector", new object[] { 1, "two" })]
    public void Equals_OtherNonElementCommandPropertiesSame_ReturnsTrue(
      string command0, string selector0, object[] args0,
      string command1, string selector1, object[] args1)
    {
      var nonElementCommand0 = new NonElementCommand(command0, selector0, args0);
      var nonElementCommand1 = new NonElementCommand(command1, selector1, args1);

      var result = nonElementCommand0.Equals(nonElementCommand1);

      result.Should().BeTrue();
    }

    [TestCase("command1", "#selector", new object[] { 1 }, "command1", "#selector", new object[] { 2 })]
    [TestCase("command2", "#selector", new object[] { 1, "two" }, "command2", "#selector", new object[] { 1, "TWO" })]
    [TestCase("command3", "#selector", new object[] { }, "command3", "#selector", new object[] { 1 })]
    [TestCase("command4", "#selector", new object[] { 1 }, "command4", "#selector", new object[] { 1, 2 })]
    [TestCase("command5", "#selector", new object[] { 2, 1 }, "command5", "#selector", new object[] { 1, 2 })]
    [TestCase("command6", "#selector", new object[] { null }, "command6", "#selector", new object[] { 1 })]
    [TestCase("command7", "#selector", new object[] { 1 }, "command7", "#selector", new object[] { 1, null })]
    public void Equals_OtherNonElementCommandHasDifferentProperties_ReturnsFalse(
      string command0, string selector0, object[] args0,
      string command1, string selector1, object[] args1)
    {
      var nonElementCommand0 = new NonElementCommand(command0, selector0, args0);
      var nonElementCommand1 = new NonElementCommand(command1, selector1, args1);

      var result = nonElementCommand0.Equals(nonElementCommand1);

      result.Should().BeFalse();
    }

    #endregion

    #region GetHashCode()

    [TestCase("command1", "#selector", new object[] { }, "command1", "#selector", new object[] { })]
    [TestCase("command2", "#selector", new object[] { }, "COMMAND2", "#selector", new object[] { })]
    [TestCase("command3", "#selector", new object[] { 1 }, "command3", "#selector", new object[] { 1 })]
    [TestCase("command4", "#selector", new object[] { null }, "command4", "#selector", new object[] { null })]
    [TestCase("command5", "#selector", new object[] { 1, "two" }, "command5", "#selector", new object[] { 1, "two" })]
    public void GetHashCode_OtherNonElementCommandPropertiesSame_ReturnsSameHashCode(
      string command0, string selector0, object[] args0,
      string command1, string selector1, object[] args1)
    {
      var nonElementCommand0 = new NonElementCommand(command0, selector0, args0);
      var nonElementCommand1 = new NonElementCommand(command1, selector1, args1);

      var result0 = nonElementCommand0.GetHashCode();
      var result1 = nonElementCommand1.GetHashCode();

      result0.Should().Be(result1);
    }

    [TestCase("command1", "#selector", new object[] { 1 }, "command1", "#selector", new object[] { 2 })]
    [TestCase("command2", "#selector", new object[] { 1, "two" }, "command2", "#selector", new object[] { 1, "TWO" })]
    [TestCase("command3", "#selector", new object[] { }, "command3", "#selector", new object[] { 1 })]
    [TestCase("command4", "#selector", new object[] { 1 }, "command4", "#selector", new object[] { 1, 2 })]
    [TestCase("command5", "#selector", new object[] { null }, "command5", "#selector", new object[] { 1 })]
    public void GetHashCode_OtherNonElementCommandHasDifferentProperties_ReturnsDifferentHashCode(
      string command0, string selector0, object[] args0,
      string command1, string selector1, object[] args1)
    {
      var nonElementCommand0 = new NonElementCommand(command0, selector0, args0);
      var nonElementCommand1 = new NonElementCommand(command1, selector1, args1);

      var result0 = nonElementCommand0.GetHashCode();
      var result1 = nonElementCommand1.GetHashCode();

      result0.Should().NotBe(result1);
    }

    #endregion
  }
}
