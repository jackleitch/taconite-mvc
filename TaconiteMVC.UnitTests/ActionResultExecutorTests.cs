using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Web.Mvc;
using System.Web.Routing;
using TaconiteMvc;

namespace TaconiteMVC.Tests
{
  [TestFixture]
  public class ActionResultExecutorTests
  {
    #region ctor(ControllerContext)

    [Test]
    public void ctor_NullControllerContext_ThrowsArgumentNullException()
    {
      Action action = () => new ActionResultExecutor(null);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion

    #region Execute(ActionResult)

    [Test]
    public void Execute_NullActionResult_ThrowsArgumentNullException()
    {
      var controllerContext = Substitute.For<ControllerContext>();
      controllerContext.RouteData = Substitute.For<RouteData>();
      controllerContext.Controller = Substitute.For<Controller>();
      var executor = new ActionResultExecutor(controllerContext);

      Action action = () => executor.Execute(null);

      action.ShouldThrow<ArgumentNullException>();
    }

    #endregion
  }
}
