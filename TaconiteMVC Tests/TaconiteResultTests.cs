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
  public class TaconiteResultTests
  {
    #region AddCommand(TaconiteCommand)

    [Test]
    public void AddCommand_AddsCommand()
    {
      var command = Substitute.For<TaconiteCommand>();
      var taconiteResult = new TaconiteResult();

      taconiteResult.AddCommand(command);

      taconiteResult.Commands.Should().HaveCount(1)
        .And.Contain(command);
    }

    [Test]
    public void AddCommand_NullCommand_ThrowsArgumentNullException()
    {
      var taconiteResult = new TaconiteResult();

      Action action = () => taconiteResult.AddCommand(null);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region ExecuteResult(ControllerContext)

    [Test]
    public void ExecuteResult_ZeroCommands_WritesCorrectTaconiteDocumentToResponse()
    {
      var controllerContext = Substitute.For<ControllerContext>();
      XElement taconiteDocument = null;
      controllerContext.HttpContext.Response.Received()
        .When(x => x.Write(Arg.Any<XElement>()))
        .Do(x => taconiteDocument = x.Arg<XElement>());
      var taconiteResult = new TaconiteResult();
      
      taconiteResult.ExecuteResult(controllerContext);

      controllerContext.HttpContext.Response.Received().ContentType = "text/xml";
      controllerContext.HttpContext.Response.Received().Write(Arg.Any<XElement>());
      taconiteDocument.Name.Should().Be((XName)"taconite");
      taconiteDocument.HasElements.Should().BeFalse();
    }

    [Test]
    public void ExecuteResult_OneCommand_WritesCorrectTaconiteDocumentToResponse()
    {
      var controllerContext = Substitute.For<ControllerContext>();
      XElement taconiteDocument = null;
      controllerContext.HttpContext.Response.Received()
        .When(x => x.Write(Arg.Any<XElement>()))
        .Do(x => taconiteDocument = x.Arg<XElement>());
      var taconiteResult = new TaconiteResult();
      var command = Substitute.For<TaconiteCommand>();
      var commandXElement = new XElement("command");
      command.CreateCommandXElement(Arg.Any<ActionResultExecutor>()).Returns(commandXElement);
      taconiteResult.AddCommand(command);

      taconiteResult.ExecuteResult(controllerContext);

      controllerContext.HttpContext.Response.Received().ContentType = "text/xml";
      controllerContext.HttpContext.Response.Received().Write(Arg.Any<XElement>());
      taconiteDocument.Name.Should().Be((XName) "taconite");
      taconiteDocument.Elements().Should().HaveCount(1)
        .And.Contain(commandXElement);
    }

    [Test]
    public void ExecuteResult_TwoCommands_WritesCorrectTaconiteDocumentToResponse()
    {
      var controllerContext = Substitute.For<ControllerContext>();
      XElement taconiteDocument = null;
      controllerContext.HttpContext.Response.Received()
        .When(x => x.Write(Arg.Any<XElement>()))
        .Do(x => taconiteDocument = x.Arg<XElement>());
      var taconiteResult = new TaconiteResult();
      var command0 = Substitute.For<TaconiteCommand>();
      var command0XElement = new XElement("command0");
      command0.CreateCommandXElement(Arg.Any<ActionResultExecutor>()).Returns(command0XElement);
      taconiteResult.AddCommand(command0);
      var command1 = Substitute.For<TaconiteCommand>();
      var command1XElement = new XElement("command1");
      command1.CreateCommandXElement(Arg.Any<ActionResultExecutor>()).Returns(command1XElement);
      taconiteResult.AddCommand(command1);

      taconiteResult.ExecuteResult(controllerContext);

      controllerContext.HttpContext.Response.Received().ContentType = "text/xml";
      controllerContext.HttpContext.Response.Received().Write(Arg.Any<XElement>());
      taconiteDocument.Name.Should().Be((XName) "taconite");
      taconiteDocument.Elements().Should().HaveCount(2)
        .And.ContainInOrder(command0XElement, command1XElement);
    }

    [Test]
    public void ExecuteResult_PassesActionResultExecutorToEachCommandWithCorrectContext()
    {
      var controllerContext = Substitute.For<ControllerContext>();
      var taconiteResult = new TaconiteResult();
      var command = Substitute.For<TaconiteCommand>();
      var commandXElement = new XElement("command");
      command.CreateCommandXElement(Arg.Any<ActionResultExecutor>()).Returns(commandXElement);
      taconiteResult.AddCommand(command);

      taconiteResult.ExecuteResult(controllerContext);

      command.Received().CreateCommandXElement(Arg.Is<ActionResultExecutor>(x => x.ControllerContext == controllerContext));
    }

    [Test]
    public void ExecuteResult_NullControllerContext_ThrowsArgumentNullException()
    {
      var taconiteResult = new TaconiteResult();

      Action action = () => taconiteResult.ExecuteResult(null);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion
  }
}
