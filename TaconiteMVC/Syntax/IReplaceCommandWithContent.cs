using System.Web.Mvc;

namespace TaconiteMvc.Syntax
{
  public interface IReplaceCommandWithContentSyntax : IFluentSyntax
  {
    TaconiteResult WithContent(string html);
    TaconiteResult WithContent(PartialViewResult partial);
  }
}
