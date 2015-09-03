using System;
using System.Web.Mvc;
using TaconiteMvc.Infrastructure;
using TaconiteMvc.Syntax;

namespace TaconiteMvc
{
  public partial class TaconiteResult
  {
    public IInsertCommandBeforeOrAfterTargetSyntax InsertContent(PartialViewResult partialViewResult)
    {
      if (partialViewResult == null)
        throw new ArgumentNullException("partialViewResult");

      var commandBuilder = new InsertCommandBuilder(this);
      commandBuilder.SetContent(partialViewResult);
      return commandBuilder;
    }

    public IInsertCommandBeforeOrAfterTargetSyntax InsertContent(string html)
    {
      if (html == null)
        throw new ArgumentNullException("html");

      var commandBuilder = new InsertCommandBuilder(this);
      commandBuilder.SetContent(html);
      return commandBuilder;
    }

    public IInsertCommandBeforeOrAfterTargetSyntax InsertPartialView()
    {
      return InsertPartialView(null, null);
    }

    public IInsertCommandBeforeOrAfterTargetSyntax InsertPartialView(string viewName)
    {
      return InsertPartialView(viewName, null);
    }

    public IInsertCommandBeforeOrAfterTargetSyntax InsertPartialView(object model)
    {
      return InsertPartialView(null, model);
    }

    public IInsertCommandBeforeOrAfterTargetSyntax InsertPartialView(string viewName, object model)
    {
      var commandBuilder = new InsertCommandBuilder(this);
      commandBuilder.SetPartialView(viewName, model);
      return commandBuilder;
    }
  }
}
