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
    private int _stylistId;

    public Client()
    {
      _id = 0;
      _name = null;
      _stylistId = 0;
    }

    public Client(string name, int stylistId, int id = 0)
    {
      _name = name;
      _stylistId = stylistId;
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
      return _stylistId;
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

    public static List<Client>GetAll()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", DB.GetConnection());
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Client> allClients = new List<Client>{};
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int stylistId = rdr.GetInt32(2);

        Client newClient = new Client(name, stylistId, id);
        allClients.Add(newClient);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();

      return allClients;
    }

    public void Save()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, stylist_id) OUTPUT INSERTED.id VALUES (@ClientName, @StylistId)", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@ClientName", this.GetName()));
      cmd.Parameters.Add(new SqlParameter("@StylistId", this.GetStylistId()));

      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();
    }

    public static Client Find(int searchId)
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @ClientId;", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@ClientId", searchId));

      SqlDataReader rdr = cmd.ExecuteReader();

      Client foundClient = new Client();
      while (rdr.Read())
      {
        foundClient._id = rdr.GetInt32(0);
        foundClient._name = rdr.GetString(1);
        foundClient._stylistId = rdr.GetInt32(2);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();

      return foundClient;
    }

    public void DeleteSingleClient()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id = @ClientId;", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@ClientId",this.GetId()));
      cmd.ExecuteNonQuery();

      DB.CloseConnection();
    }
  }
}
