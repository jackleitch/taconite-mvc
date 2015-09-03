using System;
using System.Linq;
using TaconiteMvc.Infrastructure;
using TaconiteMvc.Syntax;

namespace TaconiteMvc
{
  public partial class TaconiteResult
  {
    public IAddClassToTargetSyntax AddClass(string className)
    {
      if (String.IsNullOrEmpty(className))
        throw new ArgumentNullException("className");

      var commandBuilder = new AddClassCommandBuilder(this, className);
      return commandBuilder;
    }

    public IAddClassToTargetSyntax AddClasses(params string[] classNames)
    {
      if (!classNames.Any())
        throw new ArgumentNullException("classNames");
      if (classNames.Any(String.IsNullOrEmpty))
        throw new ArgumentNullException("classNames");

      var commandBuilder = new AddClassCommandBuilder(this, classNames);
      return commandBuilder;
    }
  }
}
