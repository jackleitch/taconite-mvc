using System;
using System.Web.Mvc;
using TaconiteMvc.Syntax;

namespace TaconiteMvc.Infrastructure
{
  /// <summary>
  /// Class used to build append commands.
  /// </summary>
  internal class AppendCommandBuilder : IAppendCommandToTargetSyntax
  {
    private string _html;
    private PartialViewResult _partialViewResult;

    /// <summary>
    /// The <see cref="TaconiteResult"/> to which this command will be added.
    /// </summary>
    protected TaconiteResult Result { get; set; }

    /// <summary>
    /// Creates a new <see cref="AppendCommandBuilder"/>.
    /// </summary>
    public AppendCommandBuilder(TaconiteResult result)
    {
      if (result == null)
        throw new ArgumentNullException("result");

      Result = result;
    }

    /// <summary>
    /// Assigns the HTML content to append.
    /// </summary>
    public void SetContent(string html)
    {
      if (html == null)
        throw new ArgumentNullException("html");

      if (_partialViewResult != null)
        throw new InvalidOperationException("PartialViewResult is already assigned.");

      _html = html;
    }

    /// <summary>
    /// Assigns the <see cref="PartialViewResult"/> to append.
    /// </summary>
    public void SetContent(PartialViewResult partialViewResult)
    {
      if (partialViewResult == null)
        throw new ArgumentNullException("partialViewResult");
      
      if (_html != null)
        throw new InvalidOperationException("HTML content is already assigned.");
      
      _partialViewResult = partialViewResult;
    }

    /// <summary>
    /// Assigns the partial view to append.
    /// </summary>
    /// <param name="viewName">Name of the view, or <c>null</c> for the default view.</param>
    /// <param name="model">The view model</param>
    public void SetPartialView(string viewName, object model)
    {
      var partialViewResult = new PartialViewResult
        {
          ViewName = viewName,
          ViewData = new ViewDataDictionary(model),
          TempData = new TempDataDictionary(),
          ViewEngineCollection = ViewEngines.Engines
        };

      SetContent(partialViewResult);
    }

    /// <summary>
    /// Specifies the jQuery selector for the target element(s).
    /// </summary>
    /// <param name="selector">The jQuery selector for the target element(s).</param>
    TaconiteResult IAppendCommandToTargetSyntax.To(string selector)
    {
      if (String.IsNullOrEmpty(selector))
        throw new ArgumentNullException("selector");

      ElementCommand command;
      if (_html != null)
        command = new ElementCommand("append", selector, _html);
      else
        command = new ElementCommand("append", selector, _partialViewResult);

      Result.AddCommand(command);

      return Result;
    }
  }
}