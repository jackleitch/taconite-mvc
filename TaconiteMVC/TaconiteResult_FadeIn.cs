using System;

namespace TaconiteMvc
{
  public partial class TaconiteResult
  {
    public TaconiteResult FadeIn(string selector)
    {
      if (String.IsNullOrEmpty(selector))
        throw new ArgumentNullException("selector");

      var command = new NonElementCommand("fadeIn", selector);
      AddCommand(command);

      return this;
    }
  }
}
