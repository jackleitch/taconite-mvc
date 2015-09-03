using System.Web.Mvc;
using TaconiteMvc.Syntax;

namespace TaconiteMvc
{
  public interface ITaconite
  {
    #region AddClass

    IAddClassToTargetSyntax AddClass(string className);

    IAddClassToTargetSyntax AddClasses(params string[] classNames);

    #endregion 

    #region Append

    /// <summary>
    /// Appends HTML content to the elements matching a jQuery selector.
    /// </summary>
    IAppendCommandToTargetSyntax AppendContent(string html);

    /// <summary>
    /// Appends the content from a <see cref="PartialViewResult"/> to the elements
    /// matching a jQuery selector.
    /// </summary>
    IAppendCommandToTargetSyntax AppendContent(PartialViewResult partialViewResult);

    /// <summary>
    /// Appends a partial view to the elements matching a jQuery selector.
    /// </summary>
    IAppendCommandToTargetSyntax AppendPartialView();

    /// <summary>
    /// Appends a partial view, using the specified model, to the elements matching 
    /// a jQuery selector.
    /// </summary>
    IAppendCommandToTargetSyntax AppendPartialView(object model);

    /// <summary>
    /// Appends a partial view, using the specified view name, to the elements
    /// matching a jQuery selector.
    /// </summary>
    IAppendCommandToTargetSyntax AppendPartialView(string viewName);

    /// <summary>
    /// Appends a partial view, using the specified view name and model, to the
    /// elements matching a jQuery selector.
    /// </summary>
    IAppendCommandToTargetSyntax AppendPartialView(string viewName, object model);

    #endregion Append

    #region Execute

    /// <summary>
    /// Executes JavaScript.
    /// </summary>
    TaconiteResult Execute(string javascript);

    /// <summary>
    /// Executes JavaScript.
    /// </summary>
    TaconiteResult Execute(JavaScriptResult javaScriptResult);

    #endregion

    #region ExecutePlugin

    /// <summary>
    /// Executes a jQuery plugin.
    /// </summary>
    IPluginCommandForTargetSyntax ExecutePlugin(string command);

    #endregion

    #region FadeIn

    TaconiteResult FadeIn(string selector);

    #endregion

    #region FadeOut

    TaconiteResult FadeOut(string selector);

    #endregion

    #region Hide

    TaconiteResult Hide(string selector);

    #endregion

    #region Insert

    IInsertCommandBeforeOrAfterTargetSyntax InsertContent(PartialViewResult partialViewResult);

    IInsertCommandBeforeOrAfterTargetSyntax InsertContent(string html);

    IInsertCommandBeforeOrAfterTargetSyntax InsertPartialView();

    IInsertCommandBeforeOrAfterTargetSyntax InsertPartialView(string viewName);

    IInsertCommandBeforeOrAfterTargetSyntax InsertPartialView(object model);

    IInsertCommandBeforeOrAfterTargetSyntax InsertPartialView(string viewName, object model);

    #endregion

    #region Prepend

    /// <summary>
    /// Prepends HTML content to the elements matching a jQuery selector.
    /// </summary>
    IPrependCommandToTargetSyntax PrependContent(string html);

    /// <summary>
    /// Prepends the content from a <see cref="PartialViewResult"/> to the elements
    /// matching a jQuery selector.
    /// </summary>
    IPrependCommandToTargetSyntax PrependContent(PartialViewResult partialViewResult);

    /// <summary>
    /// Prepends a partial view to the elements matching a jQuery selector.
    /// </summary>
    IPrependCommandToTargetSyntax PrependPartialView();

    /// <summary>
    /// Prepends a partial view, using the specified model, to the elements matching 
    /// a jQuery selector.
    /// </summary>
    IPrependCommandToTargetSyntax PrependPartialView(object model);

    /// <summary>
    /// Prepends a partial view, using the specified view name, to the elements
    /// matching a jQuery selector.
    /// </summary>
    IPrependCommandToTargetSyntax PrependPartialView(string viewName);

    /// <summary>
    /// Prepends a partial view, using the specified view name and model, to the
    /// elements matching a jQuery selector.
    /// </summary>
    IPrependCommandToTargetSyntax PrependPartialView(string viewName, object model);

    #endregion 

    #region Remove

    TaconiteResult Remove(string selector);

    #endregion

    #region RemoveAttribute

    IRemoveAttributeCommandFromTargetSyntax RemoveAttribute(string attribute);

    IRemoveAttributeCommandFromTargetSyntax RemoveAttributes(params string[] attributes);

    #endregion

    #region RemoveClass

    IRemoveClassCommandFromTargetSyntax RemoveClass(string className);

    IRemoveClassCommandFromTargetSyntax RemoveClasses(params string[] classNames);

    #endregion

    #region Replace

    /// <summary>
    /// Replaces the elements matching a jQuery selector.
    /// </summary>
    IReplaceCommandWithContentSyntax Replace(string selector);

    #endregion Replace

    #region Replace

    /// <summary>
    /// Replaces the content of elements matching a jQuery selector.
    /// </summary>
    IReplaceContentsOfCommandWithContentSyntax ReplaceContentsOf(string selector);

    #endregion Replace

    #region SetAttribute

    ISetAttributeCommandForTargetSyntax SetAttribute(string name, object value);

    ISetAttributeCommandForTargetSyntax SetAttributes(object attributes);

    #endregion

    #region Show

    TaconiteResult Show(string selector);

    #endregion

    #region SlideDown

    TaconiteResult SlideDown(string selector);

    #endregion SlideDown

    #region SlideUp

    TaconiteResult SlideUp(string selector);

    #endregion
  }
}
