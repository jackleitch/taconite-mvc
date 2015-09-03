using System;

namespace TaconiteMvc
{
  public partial class TaconiteResult
  {
    public TaconiteResult Hide(string selector)
    {
      if (String.IsNullOrEmpty(selector))
        throw new ArgumentNullException("selector");

      var command = new NonElementCommand("hide", selector);
      AddCommand(command);

      return this;
    }
  }
}
