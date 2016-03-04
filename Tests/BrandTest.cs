using Xunit;
using ShoeStores.Objects;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ShoeStores
{
  public class BrandTest //: IDisposable
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
    // public void Dispose()
    // {
    //   Brand.DeleteAll();
    //   Store.DeleteAll();
    // }//end teardown
  }//end class
}//end namespace
