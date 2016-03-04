using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace ShoeStores.Objects
{
  public class Store
  {
    private int _id;
    private string _name;

    public Store(string name, int id = 0)
    {
      _id = id;
      _name = name;
    }
/////////////////////////   Getters and Setters   /////////////////////////
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
///////////////////////   Equal Overide for testing  /////////////////////////
    public override bool Equals(System.Object otherStore)
    {
      if(!(otherStore is Store))
      {
        return false;
      }
      else
      {
        Store newStore = (Store) otherStore;
        bool idEquality = this.GetId() == newStore.GetId();
        bool nameEquality = this.GetName() == newStore.GetName();
        return (idEquality && nameEquality);
      }
    }
////////////////////////////   Create   ///////////////////////////////////
    public void Save()
    {
      //name and open the db connection
      SqlConnection conn = DB.Connection();
      conn.Open();

      //name the data reader
      SqlDataReader rdr;

      //create the query parameters
      SqlParameter name = new SqlParameter();
      name.ParameterName = "@name";
      name.Value = this.GetName();

      //create the sql command
      SqlCommand cmd = new SqlCommand("INSERT INTO stores (name) OUTPUT INSERTED.id VALUES (@name);", conn);

      //add the query parameter to the command
      cmd.Parameters.Add(name);

      //call the ExecuteReader method on the command, assign it to the data reader
      rdr = cmd.ExecuteReader();

      //call the Read method on the data reader, loop through the results
      while(rdr.Read())
      {
        //assign the id read from the OUTPUT of the query to the id of the object
        this._id = rdr.GetInt32(0);
      }

      // close the reader, close the connection
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }//end Save method
    public void Stock(Brand newBrand)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlParameter storeId = new SqlParameter();
      storeId.ParameterName = "@store_id";
      storeId.Value = this.GetId();

      SqlParameter brandId = new SqlParameter();
      brandId.ParameterName = "@brand_id";
      brandId.Value = newBrand.GetId();

      SqlCommand cmd = new SqlCommand("INSERT INTO stores_brands (store_id, brand_id) VALUES (@store_id, @brand_id);", conn);

      cmd.Parameters.Add(storeId);
      cmd.Parameters.Add(brandId);

      cmd.ExecuteNonQuery();
    }//end Stock method
/////////////////////////////   Read   ///////////////////////////////////////
    public static List<Store> GetAll()
    {
      List<Store> allStores = new List<Store> {};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stores;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Store newStore = new Store(name, id);
        allStores.Add(newStore);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allStores;
    }//end GetAll method
    public static Store Find(int id)
    {
      List<Store> allStores = Store.GetAll();

      foreach(Store item in allStores)
      {
        if(item.GetId() == id)
        {
          return item;
        }
      }
      throw new System.ArgumentException("Parameter returns no value", "id");
    }//end Find(int) method
    public List<Brand> GetBrands()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT brands.* FROM stores JOIN stores_brands ON (stores.id = stores_brands.store_id) JOIN brands ON (stores_brands.brand_id = brands.id) WHERE store_id = @id;", conn);
      SqlParameter id = new SqlParameter();
      id.ParameterName = "@id";
      id.Value = this.GetId();
      cmd.Parameters.Add(id);
      rdr = cmd.ExecuteReader();

      List<Brand> allBrands = new List<Brand> {};
      while (rdr.Read())
      {
        Brand newBrand = new Brand(rdr.GetString(1), rdr.GetInt32(0));
        allBrands.Add(newBrand);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allBrands;
    }//end GetBrands method
///////////////////////////   Update   ////////////////////////////////////////
    public void Update(string newName)
    {
      //update the name in the object
      this.SetName(newName);

      //name and open the db connection
      SqlConnection conn = DB.Connection();
      conn.Open();

      //create the query parameters
      SqlParameter name = new SqlParameter();
      name.ParameterName = "@name";
      name.Value = newName;

      SqlParameter id = new SqlParameter();
      id.ParameterName = "@id";
      id.Value = this.GetId();

      //create the sql command
      SqlCommand cmd = new SqlCommand("UPDATE stores SET name = @name WHERE id = @id;", conn);

      //add the query parameters to the command
      cmd.Parameters.Add(name);
      cmd.Parameters.Add(id);

      //execute the command
      cmd.ExecuteNonQuery();

      // close the connection
      if (conn != null)
      {
        conn.Close();
      }
    }//end Update method
///////////////////////////   Delete   ////////////////////////////////////////
    public void Unstock(Brand brand)
    {
      //name and open the db connection
      SqlConnection conn = DB.Connection();
      conn.Open();

      //create the query parameters
      SqlParameter storeId = new SqlParameter();
      storeId.ParameterName = "@store_id";
      storeId.Value = this.GetId();

      SqlParameter brandId = new SqlParameter();
      brandId.ParameterName = "@brand_id";
      brandId.Value = brand.GetId();

      //create the sql command
      SqlCommand cmd = new SqlCommand("DELETE FROM stores_brands WHERE (brand_id=@brand_id AND store_id=@store_id);", conn);

      //add the query parameters to the command
      cmd.Parameters.Add(storeId);
      cmd.Parameters.Add(brandId);

      //execute the command
      cmd.ExecuteNonQuery();

      //close the connection
      if (conn != null)
      {
        conn.Close();
      }
    }//end Unstock method
    public void Delete()
    {
      //name and open the db connection
      SqlConnection conn = DB.Connection();
      conn.Open();

      //create the query parameters
      SqlParameter id = new SqlParameter();
      id.ParameterName = "@id";
      id.Value = this.GetId();

      //create the sql command
      SqlCommand cmd = new SqlCommand("DELETE FROM stores WHERE id=@id; DELETE FROM stores_brands WHERE store_id=@id", conn);

      //add the query parameters to the command
      cmd.Parameters.Add(id);

      //execute the command
      cmd.ExecuteNonQuery();

      //close the connection
      if (conn != null)
      {
        conn.Close();
      }
    }//end Delete method
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM stores;", conn);
      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }//end DeleteAll method
  }//end class
}//end namespace
