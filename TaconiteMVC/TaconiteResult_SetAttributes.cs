using System;
using TaconiteMvc.Infrastructure;
using TaconiteMvc.Syntax;

namespace TaconiteMvc
{
  public partial class TaconiteResult
  {
    public ISetAttributeCommandForTargetSyntax SetAttribute(string name, object value)
    {
      if (String.IsNullOrEmpty(name))
        throw new ArgumentNullException("name");
      if (value == null)
        throw new ArgumentNullException("value");

      var commandBuilder = new SetAttributeCommandBuilder(this, name, value);
      return commandBuilder;
    }

    public ISetAttributeCommandForTargetSyntax SetAttributes(object attributes)
    {
      if (attributes == null)
        throw new ArgumentNullException("attributes");

      var commandBuilder = new SetAttributeCommandBuilder(this, attributes);
      return commandBuilder;
    }
  }
}
