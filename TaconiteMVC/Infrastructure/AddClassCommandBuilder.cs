using System;
using TaconiteMvc.Syntax;

namespace TaconiteMvc.Infrastructure
{
  internal class AddClassCommandBuilder : IAddClassToTargetSyntax
  {
    private readonly string[] _classNames;

    /// <summary>
    /// The <see cref="TaconiteResult"/> to which this command will be added.
    /// </summary>
    protected TaconiteResult Result { get; set; }

    public AddClassCommandBuilder(TaconiteResult result, params string[] classNames)
    {
      if (result == null)
        throw new ArgumentNullException("result");

      Result = result;
      _classNames = classNames;
    }

    /// <summary>
    /// Specifies the jQuery selector for the target element(s).
    /// </summary>
    /// <param name="selector">The jQuery selector for the target element(s).</param>
    TaconiteResult IAddClassToTargetSyntax.To(string selector)
    {
      if (String.IsNullOrEmpty(selector))
        throw new ArgumentNullException("selector");

      var classes = String.Join(" ", _classNames);
      var command = new NonElementCommand("addClass", selector, classes);

      Result.AddCommand(command);

      return Result;
    }
  }
}