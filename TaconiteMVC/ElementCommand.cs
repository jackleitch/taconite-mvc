using System;
using System.Web.Mvc;
using System.Xml.Linq;

namespace TaconiteMvc
{
  /// <summary>
  /// A Taconite command that passes its content to the Taconite method.
  /// </summary>
  public class ElementCommand : TaconiteCommand
  {
    private readonly string _command;
    private readonly string _selector;
    private readonly string _html;
    private readonly PartialViewResult _partial;

    /// <summary>
    /// The Taconite command.
    /// </summary>
    public override string Command
    {
      get { return _command; }
    }

    /// <summary>
    /// HTML content passed to the Taconite method.
    /// </summary>
    public string Html
    {
      get { return _html; }
    }

    /// <summary>
    /// Partial view result that is passed to the Taconite method.
    /// </summary>
    public PartialViewResult Partial
    {
      get { return _partial; }
    }

    /// <summary>
    /// The jQuery selector of the target element(s).
    /// </summary>
    public string Selector
    {
      get { return _selector; }
    }

    /// <summary>
    /// Creates a new <see cref="ElementCommand"/>.
    /// </summary>
    /// <param name="command">The Taconite command.</param>
    /// <param name="selector">A jQuery selector for the target element(s).</param>
    /// <param name="partial">A partial view result that is passed to the Taconite method.</param>
    public ElementCommand(string command, string selector, PartialViewResult partial)
      : this(command, selector)
    {
      _partial = partial;
    }

    /// <summary>
    /// Creates a new <see cref="ElementCommand"/>.
    /// </summary>
    /// <param name="command">The Taconite command.</param>
    /// <param name="selector">A jQuery selector for the target element(s).</param>
    /// <param name="html">HTML content to be passed to the Taconite method.</param>
    public ElementCommand(string command, string selector, string html)
      : this(command, selector)
    {
      _html = html;
    }

    private ElementCommand(string command, string selector)
    {
      _command = command;
      _selector = selector;
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

      // Use the HTML content or execute the PartialViewResult.
      var content = Html ?? executor.Execute(Partial);

      return new XElement(_command,
                          new XAttribute("select", Selector),
                          new XCData(content));
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object"/> is equal to 
    /// the current <see cref="T:System.Object"/>.
    /// </summary>
    public override bool Equals(object obj)
    {
      return Equals(obj as ElementCommand);
    }

    /// <summary>
    /// Determines whether the given <see cref="ElementCommand"/> equals this <see cref="ElementCommand"/>.
    /// </summary>
    /// <param name="other">The other <see cref="ElementCommand"/>.</param>
    /// <returns>Whether the given <see cref="ElementCommand"/> equals this <see cref="ElementCommand"/>.</returns>
    public bool Equals(ElementCommand other)
    {
      if (ReferenceEquals(null, other))
        return false;

      if (ReferenceEquals(this, other))
        return true;

      return _command.Equals(other._command, StringComparison.OrdinalIgnoreCase)
             && _selector.Equals(other._selector, StringComparison.Ordinal)
             && ((_html != null && other._html != null && _html.Equals(other._html, StringComparison.InvariantCulture))
                 || (_partial != null && other._partial != null && _partial.Equals(other._partial)));
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
               + 19*(_html == null ? 0 : _html.GetHashCode())
               + 19*(_partial == null ? 0 : _partial.GetHashCode());
      }
    }
  }
}