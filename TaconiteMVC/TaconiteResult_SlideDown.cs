using System;

namespace TaconiteMvc
{
  public partial class TaconiteResult
  {
    public TaconiteResult SlideDown(string selector)
    {
      if (String.IsNullOrEmpty(selector))
        throw new ArgumentNullException("selector");

      var command = new NonElementCommand("slideDown", selector);
      AddCommand(command);

      return this;
    }
  }
}
