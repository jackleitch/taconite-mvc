using System;

namespace TaconiteMvc
{
  public partial class TaconiteResult
  {
    public TaconiteResult Remove(string selector)
    {
      if (String.IsNullOrEmpty(selector))
        throw new ArgumentNullException("selector");

      var command = new NonElementCommand("remove", selector);
      AddCommand(command);

      return this;
    }
  }
}
