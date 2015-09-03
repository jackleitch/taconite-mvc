namespace TaconiteMvc.Syntax
{
  public interface ISetAttributeCommandForTargetSyntax : IFluentSyntax
  {
    /// <summary>
    /// Specifies the jQuery selector for the target element(s).
    /// </summary>
    /// <param name="selector">The jQuery selector for the target element(s).</param>
    TaconiteResult For(string selector);
  }
}
