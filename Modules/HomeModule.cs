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
      Post["/store/new"] = _ =>
      {
        Store newStore = new Store(Request.Form["store-name"]);
        newStore.Save();
        ModelObject model = new ModelObject();
        return View["index.cshtml", model];
      };
      Post["/brand/new"] = _ =>
      {
        Brand newBrand = new Brand(Request.Form["brand-name"]);
        newBrand.Save();
        ModelObject model = new ModelObject();
        return View["index.cshtml", model];
      };
    }
  }
}
