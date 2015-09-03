using System;
using TaconiteMvc.Syntax;

namespace TaconiteMvc.Infrastructure
{
  internal class RemoveClassCommandBuilder : IRemoveClassCommandFromTargetSyntax
  {
    private readonly string[] _classNames;
    
    /// <summary>
    /// The <see cref="TaconiteResult"/> to which this command will be added.
    /// </summary>
    protected TaconiteResult Result { get; set; }

    public RemoveClassCommandBuilder(TaconiteResult result, params string[] classNames)
    {
      if (result == null)
        throw new ArgumentNullException("result");
      if (classNames == null)
        throw new ArgumentNullException("classNames");

      Result = result;
      _classNames = classNames;
    }

    /// <summary>
    /// Specifies the jQuery selector for the target element(s).
    /// </summary>
    /// <param name="selector">The jQuery selector for the target element(s).</param>
    TaconiteResult IRemoveClassCommandFromTargetSyntax.From(string selector)
    {
      if (String.IsNullOrEmpty(selector))
        throw new ArgumentNullException("selector");

      foreach (var attribute in _classNames)
      {
        var command = new NonElementCommand("removeClass", selector, attribute);
        Result.AddCommand(command);
      }

      return Result;
    }
  }
}