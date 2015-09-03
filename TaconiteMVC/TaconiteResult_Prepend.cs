using System;
using System.Web.Mvc;
using TaconiteMvc.Infrastructure;
using TaconiteMvc.Syntax;

namespace TaconiteMvc
{
  public partial class TaconiteResult
  {
    /// <summary>
    /// Prepends HTML content to the elements matching a jQuery selector.
    /// </summary>
    public IPrependCommandToTargetSyntax PrependContent(string html)
    {
      if (html == null)
        throw new ArgumentNullException("html");

      var commandBuilder = new PrependCommandBuilder(this);
      commandBuilder.SetContent(html);
      return commandBuilder;
    }

    /// <summary>
    /// Prepends the content from a <see cref="PartialViewResult"/> to the elements
    /// matching a jQuery selector.
    /// </summary>
    public IPrependCommandToTargetSyntax PrependContent(PartialViewResult partialViewResult)
    {
      if (partialViewResult == null)
        throw new ArgumentNullException("partialViewResult");
      
      var commandBuilder = new PrependCommandBuilder(this);
      commandBuilder.SetContent(partialViewResult);
      return commandBuilder;
    }

    /// <summary>
    /// Prepends a partial view to the elements matching a jQuery selector.
    /// </summary>
    public IPrependCommandToTargetSyntax PrependPartialView()
    {
      return PrependPartialView(null, null);
    }

    /// <summary>
    /// Prepends a partial view, using the specified model, to the elements matching 
    /// a jQuery selector.
    /// </summary>
    public IPrependCommandToTargetSyntax PrependPartialView(object model)
    {
      return PrependPartialView(null, model);
    }

    /// <summary>
    /// Prepends a partial view, using the specified view name, to the elements
    /// matching a jQuery selector.
    /// </summary>
    public IPrependCommandToTargetSyntax PrependPartialView(string viewName)
    {
      return PrependPartialView(viewName, null);
    }

    /// <summary>
    /// Prepends a partial view, using the specified view name and model, to the
    /// elements matching a jQuery selector.
    /// </summary>
    public IPrependCommandToTargetSyntax PrependPartialView(string viewName, object model)
    {
      var commandBuilder = new PrependCommandBuilder(this);
      commandBuilder.SetPartialView(viewName, model);
      return commandBuilder;
    }
  }
}
