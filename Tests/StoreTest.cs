using Xunit;
using ShoeStores.Objects;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ShoeStores
{
  public class StoreTest// : IDisposable
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
    // public void Dispose()
    // {
    //   Brand.DeleteAll();
    //   Store.DeleteAll();
    // }//end teardown
  }//end class
}//end namespace
