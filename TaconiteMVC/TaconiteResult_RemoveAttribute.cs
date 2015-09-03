using System;
using System.Linq;
using TaconiteMvc.Infrastructure;
using TaconiteMvc.Syntax;

namespace TaconiteMvc
{
  public partial class TaconiteResult
  {
    public IRemoveAttributeCommandFromTargetSyntax RemoveAttribute(string attribute)
    {
      if (String.IsNullOrEmpty(attribute))
        throw new ArgumentNullException("attribute");

      var commandBuilder = new RemoveAttributeCommandBuilder(this, attribute);
      return commandBuilder;
    }

    public IRemoveAttributeCommandFromTargetSyntax RemoveAttributes(params string[] attributes)
    {
      if (attributes == null || attributes.Length == 0)
        throw new ArgumentNullException("attributes");
      if (attributes.Any(String.IsNullOrEmpty))
        throw new ArgumentNullException("attributes");

      var commandBuilder = new RemoveAttributeCommandBuilder(this, attributes);
      return commandBuilder;
    }
  }
}
