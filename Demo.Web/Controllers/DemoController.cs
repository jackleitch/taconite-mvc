using System.Web.Mvc;
using TaconiteMvc;

namespace TaconiteMVC.DemoWeb.Controllers
{
  public class DemoController : Controller
  {
    public ActionResult Index()
    {
      return View("Index");
    }

    public TaconiteResult AddClass()
    {
      return Taconite.AddClass("green").To("#addClassTarget .example-block");
    }

    public TaconiteResult Append()
    {
      return Taconite.AppendPartialView("GreenBox").To("#appendTarget");
    }

    public TaconiteResult SetAttribute()
    {
      return Taconite.SetAttribute("src", Url.Content("~/Content/Images/meerkat.jpg"))
                       .For("#setAttributeImage");
    }

    public TaconiteResult ExecuteJavaScript()
    {
      return Taconite.Execute("alert('Hello!');");
    }

    public TaconiteResult ExecutePlugin()
    {
      return Taconite.ExecutePlugin("modal").For("#executePluginModal").WithNoArguments();
    }

    public TaconiteResult FadeIn()
    {
      return Taconite.FadeIn("#fadeInTarget .example-block");
    }

    public TaconiteResult FadeOut()
    {
      return Taconite.FadeOut("#fadeOutTarget .example-block");
    }

    public TaconiteResult Hide()
    {
      return Taconite.Hide("#hideTarget .example-block");
    }

    public TaconiteResult InsertAfter()
    {
      return Taconite.InsertPartialView("GreenBox").After("#insertAfterTarget .example-block.blue");
    }

    public TaconiteResult InsertBefore()
    {
      return Taconite.InsertPartialView("GreenBox").Before("#insertBeforeTarget .example-block.blue");
    }

    public TaconiteResult Prepend()
    {
      return Taconite.PrependPartialView("GreenBox").To("#prependTarget");
    }

    public TaconiteResult Remove()
    {
      return Taconite.Remove("#removeTarget .example-block");
    }

    public TaconiteResult RemoveAttribute()
    {
      return Taconite.RemoveAttribute("title").From("#removeAttributeImage");
    }

    public TaconiteResult RemoveClass()
    {
      return Taconite.RemoveClass("blue").From("#removeClassTarget .example-block");
    }

    public TaconiteResult Replace()
    {
      return Taconite.Replace("#replaceTarget .example-block").WithPartialView("GreenBox");
    }

    public TaconiteResult ReplaceContent()
    {
      return Taconite.ReplaceContentsOf("#replaceContentTarget").WithPartialView("GreenBox");
    }

    public TaconiteResult Show()
    {
      return Taconite.Show("#showTarget .example-block");
    }

    public TaconiteResult SlideDown()
    {
      return Taconite.SlideDown("#slideDownTarget .example-block");
    }

    public TaconiteResult SlideUp()
    {
      return Taconite.SlideUp("#slideUpTarget .example-block");
    }
  }
}