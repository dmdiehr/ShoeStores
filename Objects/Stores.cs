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
  }//end class
}//end namespace
