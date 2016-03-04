using System.Collections.Generic;
using System;

namespace ShoeStores.Objects
{
  public class ModelObject
  {
    private static List<Store> _allStores = Store.GetAll();
    private static List<Brand> _allBrands = Brand.GetAll();
    private static Store _currentStore;
    private static Brand _currentBrand;

/////////////////////STATIC METHODS (SETTERS ONLY)/////////////////////////////

     public static void SetStore(Store store)
     {
       _currentStore = store;
     }
     public static void SetBrand(Brand brand)
     {
       _currentBrand = brand;
     }
    public static void UpdateLists()
    {
       _allStores = Store.GetAll();
       _allBrands = Brand.GetAll();
    }

///////////////////////INSTANCE METHODS (GETTERS ONLY)////////////////////////

    public ModelObject()
    {
      ModelObject.UpdateLists();
    }

    //Getters
    public List<Store> AllStores()
    {
      return _allStores;
    }
    public List<Brand> AllBrands()
    {
      return _allBrands;
    }
    public Store GetStore()
    {
      return _currentStore;
    }
    public Brand GetBrand()
    {
      return _currentBrand;
    }

  }
}
