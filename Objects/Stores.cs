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
    }// end Save method
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
    }// end GetAll method
///////////////////////////   Delete   ////////////////////////////////////////
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
