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
        ModelObject model = new ModelObject();
        return View["index.cshtml", model];
      };
      Get["/test2"] = _ =>
      {
        return View["test2.cshtml"];
      };
    }
  }
}
