using Nancy;
using ShoeStores.Objects;
using System.Collections.Generic;
namespace ShoeStores
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ =>
      {
        return View["test1.cshtml"];
      };
      Get["/test2"] = _ =>
      {
        return View["test2.cshtml"];
      };
    }
  }
}
