using System;
using System.Web.Mvc;
using TaconiteMvc.Infrastructure;
using TaconiteMvc.Syntax;

namespace TaconiteMvc
{
  public partial class TaconiteResult
  {
    /// <summary>
    /// Appends HTML content to the elements matching a jQuery selector.
    /// </summary>
    public IAppendCommandToTargetSyntax AppendContent(string html)
    {
      if (html == null)
        throw new ArgumentNullException("html");

      var commandBuilder = new AppendCommandBuilder(this);
      commandBuilder.SetContent(html);
      return commandBuilder;
    }

    /// <summary>
    /// Appends the content from a <see cref="PartialViewResult"/> to the elements
    /// matching a jQuery selector.
    /// </summary>
    public IAppendCommandToTargetSyntax AppendContent(PartialViewResult partialViewResult)
    {
      if (partialViewResult == null)
        throw new ArgumentNullException("partialViewResult");
      
      var commandBuilder = new AppendCommandBuilder(this);
      commandBuilder.SetContent(partialViewResult);
      return commandBuilder;
    }

    /// <summary>
    /// Appends a partial view to the elements matching a jQuery selector.
    /// </summary>
    public IAppendCommandToTargetSyntax AppendPartialView()
    {
      return AppendPartialView(null, null);
    }

    /// <summary>
    /// Appends a partial view, using the specified model, to the elements matching 
    /// a jQuery selector.
    /// </summary>
    public IAppendCommandToTargetSyntax AppendPartialView(object model)
    {
      return AppendPartialView(null, model);
    }

    /// <summary>
    /// Appends a partial view, using the specified view name, to the elements
    /// matching a jQuery selector.
    /// </summary>
    public IAppendCommandToTargetSyntax AppendPartialView(string viewName)
    {
      return AppendPartialView(viewName, null);
    }

    /// <summary>
    /// Appends a partial view, using the specified view name and model, to the
    /// elements matching a jQuery selector.
    /// </summary>
    public IAppendCommandToTargetSyntax AppendPartialView(string viewName, object model)
    {
      var commandBuilder = new AppendCommandBuilder(this);
      commandBuilder.SetPartialView(viewName, model);
      return commandBuilder;
    }
  }
}
