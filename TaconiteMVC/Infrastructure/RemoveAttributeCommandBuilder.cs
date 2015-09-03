using System;
using TaconiteMvc.Syntax;

namespace TaconiteMvc.Infrastructure
{
  internal class RemoveAttributeCommandBuilder : IRemoveAttributeCommandFromTargetSyntax
  {
    private readonly string[] _attributes;
    
    /// <summary>
    /// The <see cref="TaconiteResult"/> to which this command will be added.
    /// </summary>
    protected TaconiteResult Result { get; set; }

    public RemoveAttributeCommandBuilder(TaconiteResult result, params string[] attributes)
    {
      if (result == null)
        throw new ArgumentNullException("result");
      if (attributes == null)
        throw new ArgumentNullException("attributes");

      Result = result;
      _attributes = attributes;
    }

    /// <summary>
    /// Specifies the jQuery selector for the target element(s).
    /// </summary>
    /// <param name="selector">The jQuery selector for the target element(s).</param>
    TaconiteResult IRemoveAttributeCommandFromTargetSyntax.From(string selector)
    {
      if (String.IsNullOrEmpty(selector))
        throw new ArgumentNullException("selector");

      foreach (var attribute in _attributes)
      {
        var command = new NonElementCommand("removeAttr", selector, attribute);
        Result.AddCommand(command);
      }

      return Result;
    }
  }
}