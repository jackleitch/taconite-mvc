using System;
using System.Web.Mvc;
using System.Xml.Linq;

namespace TaconiteMvc
{
  /// <summary>
  /// A Taconite command that executes JavaScript.
  /// </summary>
  public class EvalCommand : TaconiteCommand
  {
    private readonly string _script;
    private readonly JavaScriptResult _javaScriptResult;

    /// <summary>
    /// Name of the Taconite command to execute.
    /// </summary>
    public override string Command
    {
      get { return "eval"; }
    }
    
    /// <summary>
    /// The <see cref="JavaScriptResult"/> to execute.
    /// </summary>
    public JavaScriptResult JavaScriptResult
    {
      get { return _javaScriptResult; }
    }

    /// <summary>
    /// The client script to execute.
    /// </summary>
    public string Script
    {
      get { return _script; }
    }

    /// <summary>
    /// Creates a new <see cref="EvalCommand"/>.
    /// </summary>
    /// <param name="script">The client script to execute.</param>
    public EvalCommand(string script)
    {
      _script = script;
    }

    /// <summary>
    /// Creates a new <see cref="EvalCommand"/>.
    /// </summary>
    /// <param name="javaScriptResult">The <see cref="JavaScriptResult"/> to execute.</param>
    public EvalCommand(JavaScriptResult javaScriptResult)
    {
      _javaScriptResult = javaScriptResult;
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

      var script = _script ?? executor.Execute(_javaScriptResult);

      return new XElement("eval", script);
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object"/> is equal to 
    /// the current <see cref="T:System.Object"/>.
    /// </summary>
    public override bool Equals(object obj)
    {
      return Equals(obj as EvalCommand);
    }

    /// <summary>
    /// Determines whether the given <see cref="EvalCommand"/> equals this <see cref="EvalCommand"/>.
    /// </summary>
    /// <param name="other">The other <see cref="EvalCommand"/>.</param>
    /// <returns>Whether the given <see cref="EvalCommand"/> equals this <see cref="EvalCommand"/>.</returns>
    public bool Equals(EvalCommand other)
    {
      if (ReferenceEquals(null, other))
        return false;

      if (ReferenceEquals(this, other))
        return true;

      return (_script != null && other._script != null && _script.Equals(other._script, StringComparison.InvariantCulture))
              || (_javaScriptResult != null && other._javaScriptResult != null && _javaScriptResult.Equals(other._javaScriptResult));
    }

    /// <summary>
    /// Serves as a hash function for a particular type. 
    /// </summary>
    public override int GetHashCode()
    {
      unchecked
      {
        return 17
               + 19 * (_script == null ? 0 : _script.GetHashCode())
               + 19 * (_javaScriptResult == null ? 0 : _javaScriptResult.GetHashCode());
      }
    }
  }
}
