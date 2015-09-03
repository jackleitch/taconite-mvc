namespace TaconiteMvc.Syntax
{
  public interface IRemoveClassCommandFromTargetSyntax : IFluentSyntax
  {
    /// <summary>
    /// Specifies the jQuery selector for the target element(s).
    /// </summary>
    /// <param name="selector">The jQuery selector for the target element(s).</param>
    TaconiteResult From(string selector);
  }
}
