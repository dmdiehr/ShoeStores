using Xunit;
using ShoeStores.Objects;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ShoeStores
{
  public class StoreTest : IDisposable
  {
    public StoreTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=shoe_stores_test;Integrated Security=SSPI;";
    }
    [Fact]//1
    public void EqualsOveride()
    {
      Store dummy1 = new Store("Nordstrom", 1);
      Store dummy2 = new Store("Nordstrom", 1);

      Assert.Equal(dummy1, dummy2);
    }
    [Fact]//2
    public void SaveDeleteAllGetAllDBStartsEmpty()
    {
      Store dummy = new Store("Macy's");
      List<Store> beforeList = Store.GetAll();
      dummy.Save();
      List<Store> afterList = Store.GetAll();

      Assert.Equal(beforeList.Count, 0);
      Assert.Equal(afterList.Count, 1);
    }
    [Fact]//3
    public void FindByInt_Store()
    {
      Store dummy = new Store("JC Penny");
      dummy.Save();
      int searchInt = dummy.GetId();

      Assert.Equal(dummy, Store.Find(searchInt));
    }
    [Fact]//5
    public void GetBrands_And_Stock_Test()
    {
      Store footLocker = new Store("Foot Locker");
      footLocker.Save();

      Brand nike = new Brand("Nike");
      nike.Save();
      footLocker.Stock(nike);

      List<Brand> compareList = new List<Brand> {nike};

      List<Brand> resultList = footLocker.GetBrands();

      Assert.Equal(compareList, resultList);
    }
    [Fact]//6
    public void Update_Store()
    {
      Store footLocker = new Store("footLocker");
      footLocker.Save();
      int footLockerId = footLocker.GetId();
      string beforeName = Store.Find(footLockerId).GetName();

      footLocker.Update("Foot Locker");
      string afterName = Store.Find(footLockerId).GetName();

      Assert.Equal(beforeName, "footLocker");
      Assert.Equal(afterName, "Foot Locker");
    }
    [Fact]//7
    public void Unstock_Store()
    {
      Store fredmeyer = new Store("Fred Meyer");
      fredmeyer.Save();
      Store nordstrom = new Store("Nordstrom");
      nordstrom.Save();
      Brand kswiss = new Brand("K Swiss");
      kswiss.Save();
      fredmeyer.Stock(kswiss);
      nordstrom.Stock(kswiss);

      List<Store> beforeList = kswiss.GetStores();
      List<Store> beforeCompare = new List<Store>{fredmeyer, nordstrom};

      fredmeyer.Unstock(kswiss);

      List<Store> afterList = kswiss.GetStores();
      List<Store> afterCompare = new List<Store>{nordstrom};

      Assert.Equal(beforeList, beforeCompare);
      Assert.Equal(afterList, afterCompare);
    }
    [Fact]//8
    public void Delete_Store()
    {
      Store dummy1 = new Store("Nordstrom Rack");
      dummy1.Save();
      Store dummy2 = new Store("Nordstrom");
      dummy2.Save();
      List<Store> beforeList = Store.GetAll();
      dummy1.Delete();
      List<Store> afterList = Store.GetAll();

      Assert.Equal(2, beforeList.Count);
      Assert.Equal(new List<Store>{dummy2}, afterList);
    }
    public void Dispose()
    {
      Brand.DeleteAll();
      Store.DeleteAll();
    }//end teardown
  }//end class
}//end namespace
