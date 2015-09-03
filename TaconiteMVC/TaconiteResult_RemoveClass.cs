using System;
using System.Linq;
using TaconiteMvc.Infrastructure;
using TaconiteMvc.Syntax;

namespace TaconiteMvc
{
  public partial class TaconiteResult
  {
    public IRemoveClassCommandFromTargetSyntax RemoveClass(string className)
    {
      if (String.IsNullOrEmpty(className))
        throw new ArgumentNullException("className");

      var commandBuilder = new RemoveClassCommandBuilder(this, className);
      return commandBuilder;
    }

    public IRemoveClassCommandFromTargetSyntax RemoveClasses(params string[] classNames)
    {
      if (classNames == null || classNames.Length == 0)
        throw new ArgumentNullException("classNames");
      if (classNames.Any(String.IsNullOrEmpty))
        throw new ArgumentNullException("classNames");

      var commandBuilder = new RemoveClassCommandBuilder(this, classNames);
      return commandBuilder;
    }
  }
}
