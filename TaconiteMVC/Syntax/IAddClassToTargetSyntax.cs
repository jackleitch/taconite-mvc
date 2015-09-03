namespace TaconiteMvc.Syntax
{
  public interface IAddClassToTargetSyntax : IFluentSyntax
  {
    /// <summary>
    /// Specifies the jQuery selector for the target element(s).
    /// </summary>
    /// <param name="selector">The jQuery selector for the target element(s).</param>
    TaconiteResult To(string selector);
  }
}
