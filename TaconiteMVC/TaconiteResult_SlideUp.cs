using System;

namespace TaconiteMvc
{
  public partial class TaconiteResult
  {
    public TaconiteResult SlideUp(string selector)
    {
      if (String.IsNullOrEmpty(selector))
        throw new ArgumentNullException("selector");

      var command = new NonElementCommand("slideUp", selector);
      AddCommand(command);

      return this;
    }
  }
}
