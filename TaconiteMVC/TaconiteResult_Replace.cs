using System;
using TaconiteMvc.Infrastructure;
using TaconiteMvc.Syntax;

namespace TaconiteMvc
{
  public partial class TaconiteResult
  {
    /// <summary>
    /// Replaces the elements matching a jQuery selector.
    /// </summary>
    public IReplaceCommandWithContentSyntax Replace(string selector)
    {
      if (String.IsNullOrEmpty(selector))
        throw new ArgumentNullException("selector");

      var commandBuilder = new ReplaceCommandBuilder(this);
      commandBuilder.SetSelector(selector);
      return commandBuilder;
    }
  }
}
