using System;

namespace TaconiteMvc
{
  public partial class TaconiteResult
  {
    public TaconiteResult FadeOut(string selector)
    {
      if (String.IsNullOrEmpty(selector))
        throw new ArgumentNullException("selector");

      var command = new NonElementCommand("fadeOut", selector);
      AddCommand(command);

      return this;
    }
  }
}
