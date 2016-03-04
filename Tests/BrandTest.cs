using Xunit;
using ShoeStores.Objects;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ShoeStores
{
  public class BrandTest : IDisposable
  {
    public BrandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=shoe_stores_test;Integrated Security=SSPI;";
    }
    [Fact]//1
    public void EqualsOveride()
    {
      Brand dummy1 = new Brand("Nike", 1);
      Brand dummy2 = new Brand("Nike", 1);

      Assert.Equal(dummy1, dummy2);
    }
    [Fact]//2
    public void SaveDeleteAllGetAllDBStartsEmpty()
    {
      Brand dummy = new Brand("Docs");
      List<Brand> beforeList = Brand.GetAll();
      dummy.Save();
      List<Brand> afterList = Brand.GetAll();

      Assert.Equal(beforeList.Count, 0);
      Assert.Equal(afterList.Count, 1);
    }
    [Fact]//3
    public void FindByInt_Brand()
    {
      Brand dummy = new Brand("Under Armor");
      dummy.Save();
      int searchInt = dummy.GetId();

      Assert.Equal(dummy, Brand.Find(searchInt));
    }
    [Fact]//4
    public void FindByString_Brand()
    {
      Brand dummy = new Brand("Under Armor");
      dummy.Save();

      Assert.Equal(dummy, Brand.Find("Under Armor"));
    }
    [Fact]//5
    public void GetStores_And_Stock_Test()
    {
      Store nordstrom = new Store("Nordstrom");
      nordstrom.Save();
      Store footLocker = new Store("Foot Locker");
      footLocker.Save();
      Brand nike = new Brand("Nike");
      nike.Save();
      nordstrom.Stock(nike);
      footLocker.Stock(nike);

      List<Store> compareList = new List<Store> {nordstrom, footLocker};
      List<Store> resultList = nike.GetStores();

      Assert.Equal(compareList, resultList);
    }
    [Fact]//6
    public void Update_Brand()
    {
      Brand nike = new Brand("nike");
      nike.Save();
      int nikeId = nike.GetId();
      string beforeName = Brand.Find(nikeId).GetName();

      nike.Update("Nike");
      string afterName = Brand.Find(nikeId).GetName();

      Assert.Equal(beforeName, "nike");
      Assert.Equal(afterName, "Nike");
    }
    [Fact]//7
    public void Delete_Brand()
    {
      Brand dummy1 = new Brand("Old Balance");
      dummy1.Save();
      Brand dummy2 = new Brand("New Balance");
      dummy2.Save();
      List<Brand> beforeList = Brand.GetAll();
      dummy1.Delete();
      List<Brand> afterList = Brand.GetAll();

      Assert.Equal(beforeList.Count, 2);
      Assert.Equal(afterList, new List<Brand>{dummy2});
    }
    public void Dispose()
    {
      Brand.DeleteAll();
      Store.DeleteAll();
    }//end teardown
  }//end class
}//end namespace
