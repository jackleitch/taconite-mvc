using System.Web.Mvc;
using System.Xml.Linq;

namespace TaconiteMvc
{
  /// <summary>
  /// A single Taconite command.
  /// </summary>
  public abstract class TaconiteCommand
  {
    /// <summary>
    /// Name of the Taconite command to execute.
    /// </summary>
    public abstract string Command { get; }

    /// <summary>
    /// Creates a <see cref="XElement"/> for this command.
    /// </summary>
    /// <param name="executor">Executor for <see cref="ActionResult"/>s.</param>
    /// <returns>A <see cref="XElement"/> for this command.</returns>
    internal abstract XElement CreateCommandXElement(ActionResultExecutor executor);
  }
}
