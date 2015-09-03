using System;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace TaconiteMvc
{
  /// <summary>
  /// Class used to execute action results.
  /// </summary>
  internal class ActionResultExecutor
  {
    private readonly ControllerContext _controllerContext;

    /// <summary>
    /// The <see cref="ControllerContext"/> used to execute action results.
    /// </summary>
    public ControllerContext ControllerContext
    {
      get { return _controllerContext; }
    }

    /// <summary>
    /// Creates a new <see cref="ActionResultExecutor"/>.
    /// </summary>
    public ActionResultExecutor(ControllerContext controllerContext)
    {
      if (controllerContext == null)
        throw new ArgumentNullException("controllerContext");

      _controllerContext = controllerContext;
    }

    /// <summary>
    /// Executes the given <see cref="ActionResult"/>.
    /// </summary>
    /// <param name="actionResult">The action result to execute.</param>
    /// <returns>The result of executing the action result.</returns>
    public virtual string Execute(ActionResult actionResult)
    {
      if (actionResult == null)
        throw new ArgumentNullException("actionResult");

      // Build a new ControllerContext, using a StringWriter to capture the result of executing the ActionResult.
      using (var writer = new StringWriter(CultureInfo.InvariantCulture))
      {
        var response = new HttpResponse(writer);
        var context = new HttpContext(_controllerContext.HttpContext.ApplicationInstance.Request, response);
        var controllerContext = new ControllerContext(new HttpContextWrapper(context), _controllerContext.RouteData, _controllerContext.Controller);

        var oldContext = HttpContext.Current;
        HttpContext.Current = context;

        actionResult.ExecuteResult(controllerContext);

        HttpContext.Current = oldContext;

        writer.Flush();

        return writer.ToString();
      }
    }
  }
}
