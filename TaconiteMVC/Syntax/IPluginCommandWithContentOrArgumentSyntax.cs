using System.Web.Mvc;

namespace TaconiteMvc.Syntax
{
  public interface IPluginCommandWithContentOrArgumentSyntax : IFluentSyntax
  {
    TaconiteResult WithContent(string html);

    TaconiteResult WithContent(PartialViewResult partial);

    TaconiteResult WithPartialView();

    TaconiteResult WithPartialView(string viewName);

    TaconiteResult WithPartialView(object model);

    TaconiteResult WithPartialView(string viewModel, object model);

    TaconiteResult WithArguments(params object[] arguments);

    TaconiteResult WithNoArguments();
  }
}
