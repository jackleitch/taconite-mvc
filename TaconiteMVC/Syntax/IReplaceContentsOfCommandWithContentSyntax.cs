using System.Web.Mvc;

namespace TaconiteMvc.Syntax
{
  public interface IReplaceContentsOfCommandWithContentSyntax : IFluentSyntax
  {
    TaconiteResult WithContent(string html);

    TaconiteResult WithContent(PartialViewResult partialViewResult);

    TaconiteResult WithPartialView();

    TaconiteResult WithPartialView(object model);

    TaconiteResult WithPartialView(string viewName);

    TaconiteResult WithPartialView(string viewName, object model);
  }
}
