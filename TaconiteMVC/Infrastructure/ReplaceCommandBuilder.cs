using System;
using System.Web.Mvc;
using TaconiteMvc.Syntax;

namespace TaconiteMvc.Infrastructure
{
  /// <summary>
  /// Class used to build replace commands.
  /// </summary>
  public class ReplaceCommandBuilder : IReplaceCommandWithContentSyntax
  {
    private string _selector;

    /// <summary>
    /// The <see cref="TaconiteResult"/> to which this command will be added.
    /// </summary>
    protected TaconiteResult Result { get; set; }

    /// <summary>
    /// Creates a new <see cref="ReplaceCommandBuilder"/>.
    /// </summary>
    public ReplaceCommandBuilder(TaconiteResult result)
    {
      if (result == null)
        throw new ArgumentNullException("result");

      Result = result;
    }

    /// <summary>
    /// Assigns the jQuery selector of the elements to replace.
    /// </summary>
    public void SetSelector(string selector)
    {
      if (selector == null)
        throw new ArgumentNullException("selector");

      _selector = selector;
    }

    public TaconiteResult WithContent(string html)
    {
      if (String.IsNullOrEmpty(html))
        throw new ArgumentNullException("html");

      var command = new ElementCommand("replace", _selector, html);
      Result.AddCommand(command);

      return Result;
    }

    public TaconiteResult WithContent(PartialViewResult partialViewResult)
    {
      if (partialViewResult == null)
        throw new ArgumentNullException("partialViewResult");

      var command = new ElementCommand("replace", _selector, partialViewResult);
      Result.AddCommand(command);

      return Result;
    }

    public TaconiteResult WithPartialView()
    {
      return WithPartialView(null, null);
    }

    public TaconiteResult WithPartialView(object model)
    {
      return WithPartialView(null, model);
    }

    public TaconiteResult WithPartialView(string viewName)
    {
      return WithPartialView(viewName, null);
    }

    public TaconiteResult WithPartialView(string viewName, object model)
    {
      var partialViewResult = new PartialViewResult
        {
          ViewName = viewName,
          ViewData = new ViewDataDictionary(model),
          TempData = new TempDataDictionary(),
          ViewEngineCollection = ViewEngines.Engines
        };

      return WithContent(partialViewResult);
    }
  }
}
