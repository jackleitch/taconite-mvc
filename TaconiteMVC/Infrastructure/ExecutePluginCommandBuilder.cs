using System;
using System.Web.Mvc;
using TaconiteMvc.Syntax;

namespace TaconiteMvc.Infrastructure
{
  public class ExecutePluginCommandBuilder
    : IPluginCommandForTargetSyntax, IPluginCommandWithContentOrArgumentSyntax
  {
    private string _plugin;
    private string _selector;

    /// <summary>
    /// The <see cref="TaconiteResult"/> to which this command will be added.
    /// </summary>
    protected TaconiteResult Result { get; set; }

    /// <summary>
    /// Creates a new <see cref="ExecutePluginCommandBuilder"/>.
    /// </summary>
    public ExecutePluginCommandBuilder(ITaconite taconite)
    {
      Result = taconite as TaconiteResult ?? new TaconiteResult();
    }
    
    /// <summary>
    /// Assigns the plugin command name.
    /// </summary>
    public void SetPlugin(string plugin)
    {
      if (String.IsNullOrEmpty(plugin))
        throw new ArgumentNullException("plugin");

      _plugin = plugin;
    }

    IPluginCommandWithContentOrArgumentSyntax IPluginCommandForTargetSyntax.For(string selector)
    {
      if (String.IsNullOrEmpty(selector))
        throw new ArgumentNullException("selector");

      _selector = selector;

      return this;
    }

    TaconiteResult IPluginCommandWithContentOrArgumentSyntax.WithContent(string html)
    {
      if (html == null)
        throw new ArgumentNullException("html");

      var command = new ElementCommand(_plugin, _selector, html);
      Result.AddCommand(command);

      return Result;
    }

    TaconiteResult IPluginCommandWithContentOrArgumentSyntax.WithContent(PartialViewResult partial)
    {
      if (partial == null)
        throw new ArgumentNullException("partial");

      var command = new ElementCommand(_plugin, _selector, partial);
      Result.AddCommand(command);

      return Result;
    }

    public TaconiteResult WithPartialView()
    {
      return WithPartialView(null, null);
    }

    public TaconiteResult WithPartialView(string viewName)
    {
      return WithPartialView(viewName, null);
    }

    public TaconiteResult WithPartialView(object model)
    {
      return WithPartialView(null, model);
    }

    public TaconiteResult WithPartialView(string viewName, object model)
    {
      var partialViewResult = new PartialViewResult
        {
          ViewName = viewName,
          ViewData = new ViewDataDictionary(model),
          TempData = new TempDataDictionary(),
          ViewEngineCollection = ViewEngines.Engines
        };

      var command = new ElementCommand(_plugin, _selector, partialViewResult);
      Result.AddCommand(command);

      return Result;
    }

    TaconiteResult IPluginCommandWithContentOrArgumentSyntax.WithArguments(params object[] arguments)
    {
      var command = new NonElementCommand(_plugin, _selector, arguments);
      Result.AddCommand(command);

      return Result;
    }

    TaconiteResult IPluginCommandWithContentOrArgumentSyntax.WithNoArguments()
    {
      var command = new NonElementCommand(_plugin, _selector);
      Result.AddCommand(command);

      return Result;
    }
  }
}
