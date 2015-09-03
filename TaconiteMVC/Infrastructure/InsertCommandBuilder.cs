using System;
using System.Web.Mvc;
using TaconiteMvc.Syntax;

namespace TaconiteMvc.Infrastructure
{
  public class InsertCommandBuilder : IInsertCommandBeforeOrAfterTargetSyntax
  {
    private string _html;
    private PartialViewResult _partialViewResult;

    /// <summary>
    /// The <see cref="TaconiteResult"/> to which this command will be added.
    /// </summary>
    protected TaconiteResult Result { get; set; }

    public InsertCommandBuilder(TaconiteResult result)
    {
      if (result == null)
        throw new ArgumentNullException("result");

      Result = result;
    }

    /// <summary>
    /// Assigns the HTML content to insert.
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
    /// Assigns the <see cref="PartialViewResult"/> to insert.
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
    /// Assigns the partial view to insert.
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

    TaconiteResult IInsertCommandBeforeOrAfterTargetSyntax.After(string selector)
    {
      if (String.IsNullOrEmpty(selector))
        throw new ArgumentNullException("selector");

      ElementCommand command;
      if (_html != null)
        command = new ElementCommand("after", selector, _html);
      else
        command = new ElementCommand("after", selector, _partialViewResult);

      Result.AddCommand(command);

      return Result;
    }

    TaconiteResult IInsertCommandBeforeOrAfterTargetSyntax.Before(string selector)
    {
      if (String.IsNullOrEmpty(selector))
        throw new ArgumentNullException("selector");

      ElementCommand command;
      if (_html != null)
        command = new ElementCommand("before", selector, _html);
      else
        command = new ElementCommand("before", selector, _partialViewResult);

      Result.AddCommand(command);

      return Result;
    }
  }
}
