using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HairSalon;

namespace HairSalon.Objects
{
  public class Client
  {
    private int _id;
    private string _name;
    private int _stylist_id;

    public Client(string name, int stylist_id, int id = 0)
    {
      _name = name;
      _stylist_id = stylist_id;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public int GetStylistId()
    {
      return _stylist_id;
    }

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        return (this.GetId() == newClient.GetId() &&
                this.GetName() == newClient.GetName() &&
                this.GetStylistId() == newClient.GetStylistId());
      }
    }
 }
}
