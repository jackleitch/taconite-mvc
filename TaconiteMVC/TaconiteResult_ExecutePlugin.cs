using System;
using TaconiteMvc.Infrastructure;
using TaconiteMvc.Syntax;

namespace TaconiteMvc
{
  public partial class TaconiteResult
  {
    public IPluginCommandForTargetSyntax ExecutePlugin(string command)
    {
      if (String.IsNullOrEmpty(command))
        throw new ArgumentNullException("command");

      var commandBuilder = new ExecutePluginCommandBuilder(this);
      commandBuilder.SetPlugin(command);
      return commandBuilder;
    }
  }
}
