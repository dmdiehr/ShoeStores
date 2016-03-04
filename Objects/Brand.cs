using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace ShoeStores.Objects
{
  public class Brand
  {
    private int _id;
    private string _name;

    public Brand(string name, int id = 0)
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
////////////////////////   Equal Overide for testing   /////////////////////////
    public override bool Equals(System.Object otherBrand)
    {
      if(!(otherBrand is Brand))
      {
        return false;
      }
      else
      {
        Brand newBrand = (Brand) otherBrand;
        bool idEquality = this.GetId() == newBrand.GetId();
        bool nameEquality = this.GetName() == newBrand.GetName();
        return (idEquality && nameEquality);
      }
    }// end Equals Overide
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
      SqlCommand cmd = new SqlCommand("INSERT INTO brands (name) OUTPUT INSERTED.id VALUES (@name);", conn);

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
    }// end Save method
/////////////////////////////   Read   ///////////////////////////////////////
    public static List<Brand> GetAll()
    {
      List<Brand> allBrands = new List<Brand> {};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM brands;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Brand newBrand = new Brand(name, id);
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
    }//end GetAll method
    public static Brand Find(int id)
    {
      List<Brand> allBrands = Brand.GetAll();

      foreach(Brand item in allBrands)
      {
        if(item.GetId() == id)
        {
          return item;
        }
      }
      throw new System.ArgumentException("Parameter returns no value", "id");
    }//end Find(int) method
    public static Brand Find(string name)
    {
      List<Brand> allBrands = Brand.GetAll();

      foreach(Brand item in allBrands)
      {
        if(item.GetName() == name)
        {
          return item;
        }
      }
      throw new System.ArgumentException("Parameter returns no value", "id");
    }//end Find(string method)
///////////////////////////   Delete   ////////////////////////////////////////
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
      SqlCommand cmd = new SqlCommand("DELETE FROM brands WHERE id=@id; DELETE FROM stores_brands WHERE brand_id=@id", conn);

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
      SqlCommand cmd = new SqlCommand("DELETE FROM brands;", conn);
      cmd.ExecuteNonQuery();
    }// end DeleteAll method
  }//end class
}//end namespace
