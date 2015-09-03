using System;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;

namespace TaconiteMvc
{
  /// <summary>
  /// A Taconite command that passes arguments to the Taconite method.
  /// </summary>
  public class NonElementCommand : TaconiteCommand
  {
    private readonly string _command;
    private readonly string _selector;
    private readonly object[] _arguments;

    /// <summary>
    /// The function arguments, in order.
    /// </summary>
    public object[] Arguments
    {
      get { return _arguments; }
    }

    /// <summary>
    /// Name of the Taconite command to execute.
    /// </summary>
    public override string Command
    {
      get { return _command; }
    }

    /// <summary>
    /// The jQuery selector of the target element(s).
    /// </summary>
    public string Selector
    {
      get { return _selector; }
    }

    /// <summary>
    /// Creates a new <see cref="NonElementCommand"/>.
    /// </summary>
    /// <param name="command">The Taconite command.</param>
    /// <param name="selector">A jQuery selector for the target element(s).</param>
    /// <param name="arguments">The function arguments, in order.</param>
    public NonElementCommand(string command, string selector, params object[] arguments)
    {
      _command = command;
      _selector = selector;
      _arguments = arguments;
    }

    /// <summary>
    /// Creates a <see cref="XElement"/> for this command.
    /// </summary>
    /// <param name="executor">Executor for <see cref="ActionResult"/>s.</param>
    /// <returns>A <see cref="XElement"/> for this command.</returns>
    internal override XElement CreateCommandXElement(ActionResultExecutor executor)
    {
      if (executor == null)
        throw new ArgumentNullException("executor");

      var xElement = new XElement(_command,
                                  new XAttribute("select", _selector),
                                  _arguments.Where(x => x != null).Select((x, i) => new XAttribute("arg" + (i + 1), x)));

      return xElement;
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object"/> is equal to 
    /// the current <see cref="T:System.Object"/>.
    /// </summary>
    public override bool Equals(object obj)
    {
      return Equals(obj as NonElementCommand);
    }

    /// <summary>
    /// Determines whether the given <see cref="NonElementCommand"/> equals this <see cref="NonElementCommand"/>.
    /// </summary>
    /// <param name="other">The other <see cref="NonElementCommand"/>.</param>
    /// <returns>Whether the given <see cref="NonElementCommand"/> equals this <see cref="NonElementCommand"/>.</returns>
    public bool Equals(NonElementCommand other)
    {
      if (ReferenceEquals(null, other))
        return false;

      if (ReferenceEquals(this, other))
        return true;

      return _command.Equals(other._command, StringComparison.OrdinalIgnoreCase)
             && _selector.Equals(other._selector, StringComparison.Ordinal)
             && _arguments.Length == other._arguments.Length
             && _arguments.Zip(other._arguments, (x, y) => new { A = x, B = y }).All((x => (x.A == null && x.B == null) || (x.A != null && x.B != null && x.A.Equals(x.B))));
    }

    /// <summary>
    /// Serves as a hash function for a particular type. 
    /// </summary>
    public override int GetHashCode()
    {
      unchecked
      {
        return 17
               + 19*_command.ToLowerInvariant().GetHashCode()
               + 19*_selector.GetHashCode()
               + _arguments.Sum(x => x == null ? 0 : x.GetHashCode());
      }
    }
  }
}
