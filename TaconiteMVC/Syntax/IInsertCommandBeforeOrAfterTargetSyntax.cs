namespace TaconiteMvc.Syntax
{
  public interface IInsertCommandBeforeOrAfterTargetSyntax
  {
    TaconiteResult After(string selector);

    TaconiteResult Before(string selector);
  }
}
