using System;
using System.Web.Mvc;
using TaconiteMvc.Infrastructure;
using TaconiteMvc.Syntax;

namespace TaconiteMvc
{
  public partial class TaconiteResult
  {
    public TaconiteResult Execute(string script)
    {
      if (String.IsNullOrEmpty(script))
        throw new ArgumentNullException("script");

      var command = new EvalCommand(script);
      AddCommand(command);

      return this;
    }

    public TaconiteResult Execute(JavaScriptResult javaScriptResult)
    {
      if (javaScriptResult == null)
        throw new ArgumentNullException("javaScriptResult");

      var command = new EvalCommand(javaScriptResult);
      AddCommand(command);

      return this;
    }
  }
}
