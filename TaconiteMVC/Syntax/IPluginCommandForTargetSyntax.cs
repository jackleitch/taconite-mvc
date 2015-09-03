namespace TaconiteMvc.Syntax
{
  public interface IPluginCommandForTargetSyntax : IFluentSyntax
  {
    IPluginCommandWithContentOrArgumentSyntax For(string targetSelector);
  }
}
