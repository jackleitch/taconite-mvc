using System;

namespace TaconiteMvc
{
  public partial class TaconiteResult
  {
    public TaconiteResult Show(string selector)
    {
      if (String.IsNullOrEmpty(selector))
        throw new ArgumentNullException("selector");

      var command = new NonElementCommand("show", selector);
      AddCommand(command);

      return this;
    }
  }
}
