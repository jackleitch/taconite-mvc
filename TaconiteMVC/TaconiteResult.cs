using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;

namespace TaconiteMvc
{
  /// <summary>
  /// The result of an action method that is used to render a Taconite document.
  /// </summary>
  public partial class TaconiteResult : ActionResult, ITaconite
  {
    private readonly List<TaconiteCommand> _commands = new List<TaconiteCommand>();

    /// <summary>
    /// The commands in this result, in order of client execution.
    /// </summary>
    public IEnumerable<TaconiteCommand> Commands
    {
      get { return _commands; }
    }

    /// <summary>
    /// Adds the specified command to this <see cref="TaconiteResult"/>.
    /// </summary>
    /// <param name="command">The command to add.</param>
    public void AddCommand(TaconiteCommand command)
    {
      if (command == null)
        throw new ArgumentNullException("command");

      _commands.Add(command);
    }

    /// <summary>
    /// Processes this <see cref="TaconiteResult"/>, writing the resulting Taconite
    /// document to the response.
    /// </summary>
    /// <param name="context">The context in which the result is executed.</param>
    public override void ExecuteResult(ControllerContext context)
    {
      if (context == null)
        throw new ArgumentNullException("context");

      // Create an ActionResultExecutor that commands can use to execute other ActionResults.
      var executor = new ActionResultExecutor(context);

      // Create the document root element containing the command elements for the commands.
      var rootElement = new XElement("taconite",
                                     Commands.Select(c => c.CreateCommandXElement(executor)));

      // Write the resulting Taconite document XML to the response.
      context.HttpContext.Response.ContentType = "text/xml";
      context.HttpContext.Response.Write(rootElement);
    }
  }
}