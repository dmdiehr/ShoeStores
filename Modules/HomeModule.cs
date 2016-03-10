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
      Get["/store/{id}"] = parameters =>
      {
        Store thisStore = Store.Find(parameters.id);
        ModelObject.SetStore(thisStore);
        ModelObject model = new ModelObject();
        return View["store.cshtml", model];
      };
      Get["/brand/{id}"] = parameters =>
      {
        Brand thisBrand = Brand.Find(parameters.id);
        ModelObject.SetBrand(thisBrand);
        ModelObject model = new ModelObject();
        return View["brand.cshtml", model];
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
      Post["/update"] = _ =>
      {
        Store thisStore = Store.Find(Request.Form["update-store"]);
        Brand thisBrand = Brand.Find(Request.Form["update-brand"]);
        thisStore.Stock(thisBrand);
        ModelObject model = new ModelObject();
        return View["index.cshtml", model];
      };
      Get["/update/{storeId}/{brandId}"] = parameters => {
        Store thisStore = Store.Find(parameters.storeId);
        Brand thisBrand = Brand.Find(parameters.brandId);
        thisStore.Unstock(thisBrand);
        ModelObject model = new ModelObject();
        return View["index.cshtml", model];
      };
      Post["/edit/{id}"] = parameters =>
      {
        Store thisStore = Store.Find(parameters.id);
        thisStore.Update(Request.Form["edit-store"]);
        ModelObject model = new ModelObject();
        return View["index.cshtml", model];
      };
    }
  }
}
