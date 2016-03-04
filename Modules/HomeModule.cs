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
      Get["/store/delete/{id}"] = parameters =>
      {
        Store thisStore = Store.Find(parameters.id);
        thisStore.Delete();
        ModelObject model = new ModelObject();
        return View["index.cshtml", model];
      };
      Get["/brand/delete/{id}"] = parameters =>
      {
        Brand thisBrand = Brand.Find(parameters.id);
        thisBrand.Delete();
        ModelObject model = new ModelObject();
        return View["index.cshtml", model];
      };
      Get["/store/empty"] = _ =>
      {
        Store.DeleteAll();
        ModelObject model = new ModelObject();
        return View["index.cshtml", model];
      };
      Get["/brand/empty"] = _ =>
      {
        Brand.DeleteAll();
        ModelObject model = new ModelObject();
        return View["index.cshtml", model];
      };
    }
  }
}
