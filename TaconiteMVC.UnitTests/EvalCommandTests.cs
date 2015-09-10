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
  public class EvalCommandTests
  {
    #region CreateCommandXElement(ActionResultExecutor)

    [Test]
    public void CreateCommandXElement_RawScript_ReturnsCorrectXElement()
    {
      var script = "console.log('weeeeeee')";
      var evalCommand = new EvalCommand(script);
      var controllerContext = Substitute.For<ControllerContext>();
      var actionResultExecutor = Substitute.For<ActionResultExecutor>(controllerContext);

      var result = evalCommand.CreateCommandXElement(actionResultExecutor);

      result.Name.Should().Be((XName) "eval");
      result.Value.Should().Be(script);
    }

    [Test]
    public void CreateCommandXElement_JavaScriptResult_ReturnsCorrectXElement()
    {
      var javaScriptResult = new JavaScriptResult();
      var evalCommand = new EvalCommand(javaScriptResult);
      var controllerContext = Substitute.For<ControllerContext>();
      var script = "console.log('weeeeeee')";
      var actionResultExecutor = Substitute.For<ActionResultExecutor>(controllerContext);
      actionResultExecutor.Execute(javaScriptResult).Returns(script);

      var result = evalCommand.CreateCommandXElement(actionResultExecutor);

      result.Name.Should().Be((XName)"eval");
      result.Value.Should().Be(script);
    }

    [Test]
    public void CreateCommandXElement_NullActionResultExecutor_ThrowsArgumentNullException()
    {
      var evalCommand = new EvalCommand("script");

      Action action = () => evalCommand.CreateCommandXElement(null);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Equals(object)

    [Test]
    public void Equals_OtherObjectIsNotAnEvalCommand_ReturnsFalse()
    {
      var other = new object();
      var evalCommand = new EvalCommand("script");

      var result = evalCommand.Equals(other);

      result.Should().BeFalse();
    }

    #endregion

    #region Equals(ElementCommand)

    [Test]
    public void Equals_OtherEvalCommandIsSameObject_ReturnsTrue()
    {
      var script = "console.log('weeeeeee')";
      var evalCommand = new EvalCommand(script);

      var result = evalCommand.Equals(evalCommand);

      result.Should().BeTrue();
    }

    [Test]
    public void Equals_OtherEvalCommandIsNull_ReturnsFalse()
    {
      var script = "console.log('weeeeeee')";
      var evalCommand0 = new EvalCommand(script);
      EvalCommand evalCommand1 = null;

      var result = evalCommand0.Equals(evalCommand1);

      result.Should().BeFalse();
    }

    [Test]
    public void Equals_OtherEvalCommandHasSameScript_ReturnsTrue()
    {
      var script = "console.log('weeeeeee')";
      var evalCommand0 = new EvalCommand(script);
      var evalCommand1 = new EvalCommand(script);

      var result = evalCommand0.Equals(evalCommand1);

      result.Should().BeTrue();
    }

    [Test]
    public void Equals_OtherEvalCommandHasDifferentScript_ReturnsFalse()
    {
      var script0 = "console.log('weeeeeee')";
      var script1 = "console.log('no.')";
      var evalCommand0 = new EvalCommand(script0);
      var evalCommand1 = new EvalCommand(script1);

      var result = evalCommand0.Equals(evalCommand1);

      result.Should().BeFalse();
    }

    [Test]
    public void Equals_OtherEvalCommandHasSameJavaScriptResult_ReturnsTrue()
    {
      var javaScriptResult = new JavaScriptResult();
      var evalCommand0 = new EvalCommand(javaScriptResult);
      var evalCommand1 = new EvalCommand(javaScriptResult);

      var result = evalCommand0.Equals(evalCommand1);

      result.Should().BeTrue();
    }

    [Test]
    public void Equals_OtherEvalCommandHasDifferentJavaScriptResult_ReturnsFalse()
    {
      var javaScriptResult0 = new JavaScriptResult();
      var javaScriptResult1 = new JavaScriptResult();
      var evalCommand0 = new EvalCommand(javaScriptResult0);
      var evalCommand1 = new EvalCommand(javaScriptResult1);

      var result = evalCommand0.Equals(evalCommand1);

      result.Should().BeFalse();
    }

    [Test]
    public void Equals_OneEvalCommandHasScriptAndOtherHasJavaScriptResult_ReturnsFalse()
    {
      var script = "console.log('weeeeeee')";
      var javaScriptResult = new JavaScriptResult();
      var evalCommand0 = new EvalCommand(script);
      var evalCommand1 = new EvalCommand(javaScriptResult);

      var result = evalCommand0.Equals(evalCommand1);

      result.Should().BeFalse();
    }

    #endregion

    #region GetHashCode()

    [Test]
    public void GetHashCode_EvalCommandsHaveSameScript_ReturnsSameHashCode()
    {
      var script = "console.log('weeeeeee')";
      var evalCommand0 = new EvalCommand(script);
      var evalCommand1 = new EvalCommand(script);

      var result0 = evalCommand0.GetHashCode();
      var result1 = evalCommand1.GetHashCode();

      result0.Should().Be(result1);
    }

    [Test]
    public void GetHashCode_EvalCommandsHaveDifferentScripts_ReturnsDifferentHashCodes()
    {
      var script0 = "console.log('weeeeeee')";
      var script1 = "console.log('no.')";
      var evalCommand0 = new EvalCommand(script0);
      var evalCommand1 = new EvalCommand(script1);

      var result0 = evalCommand0.GetHashCode();
      var result1 = evalCommand1.GetHashCode();

      result0.Should().NotBe(result1);
    }

    [Test]
    public void GetHashCode_EvalCommandsHaveSameJavaScriptResult_ReturnsSameHashCode()
    {
      var javaScriptResult = new JavaScriptResult();
      var evalCommand0 = new EvalCommand(javaScriptResult);
      var evalCommand1 = new EvalCommand(javaScriptResult);

      var result0 = evalCommand0.GetHashCode();
      var result1 = evalCommand1.GetHashCode();

      result0.Should().Be(result1);
    }

    [Test]
    public void GetHashCode_EvalCommandsHaveDifferentJavaScriptResults_ReturnsDifferentHashCodes()
    {
      var javaScriptResult0 = new JavaScriptResult();
      var javaScriptResult1 = new JavaScriptResult();
      var evalCommand0 = new EvalCommand(javaScriptResult0);
      var evalCommand1 = new EvalCommand(javaScriptResult1);

      var result0 = evalCommand0.GetHashCode();
      var result1 = evalCommand1.GetHashCode();

      result0.Should().NotBe(result1);
    }

    [Test]
    public void GetHashCode_EvalCommandHasScriptAndOtherHasJavaScriptResult_ReturnsDifferentHashCodes()
    {
      var script = "console.log('weeeeeee')";
      var javaScriptResult = new JavaScriptResult();
      var evalCommand0 = new EvalCommand(script);
      var evalCommand1 = new EvalCommand(javaScriptResult);

      var result0 = evalCommand0.GetHashCode();
      var result1 = evalCommand1.GetHashCode();

      result0.Should().NotBe(result1);
    }

    #endregion
  }
}
