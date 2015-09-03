using System;
using TaconiteMvc.Infrastructure;
using TaconiteMvc.Syntax;

namespace TaconiteMvc
{
  public partial class TaconiteResult
  {
    public IReplaceContentsOfCommandWithContentSyntax ReplaceContentsOf(string selector)
    {
      if (String.IsNullOrEmpty(selector))
        throw new ArgumentNullException("selector");

      var commandBuilder = new ReplaceContentsOfCommandBuilder(this);
      commandBuilder.SetSelector(selector);
      return commandBuilder;
    }
  }
}
