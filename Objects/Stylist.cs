using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HairSalon;

namespace HairSalon.Objects
{
  public class Stylist
  {
    private int _id;
    private string _name;
    private int _rating;

    public Stylist()
    {
      _id = 0;
      _name = null;
      _rating = 0;
    }
    public Stylist(string name, int rating, int id = 0)
    {
      _name = name;
      _rating = rating;
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
    public int GetRating()
    {
      return _rating;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        return (this.GetId() == newStylist.GetId() &&
                this.GetName() == newStylist.GetName() &&
                this.GetRating() == newStylist.GetRating());
      }
    }

    public static List<Stylist>GetAll()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", DB.GetConnection());
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Stylist> allStylists = new List<Stylist>{};
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int rating = rdr.GetInt32(2);

        Stylist newStylist = new Stylist(name, rating, id);
        allStylists.Add(newStylist);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();

      return allStylists;
    }

    public void Save()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("INSERT INTO stylists (name, rating) OUTPUT INSERTED.id VALUES (@StylistName, @StylistRating)", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@StylistName", this.GetName()));
      cmd.Parameters.Add(new SqlParameter("@StylistRating", this.GetRating()));

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

    public static void DeleteAll()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("DELETE FROM stylists; DELETE FROM clients", DB.GetConnection());
      cmd.ExecuteNonQuery();
      DB.CloseConnection();
    }

    public static Stylist Find(int searchId)
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id = @StylistId;", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@StylistId", searchId));

      SqlDataReader rdr = cmd.ExecuteReader();

      Stylist foundStylist = new Stylist();
      while (rdr.Read())
      {
        foundStylist._id = rdr.GetInt32(0);
        foundStylist._name = rdr.GetString(1);
        foundStylist._rating = rdr.GetInt32(2);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();

      return foundStylist;
    }

    public void DeleteSingleStylist()
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("DELETE FROM stylists WHERE id = @StylistId;", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@StylistId",this.GetId()));
      cmd.ExecuteNonQuery();

      DB.CloseConnection();
    }

    public static List<Stylist> SearchByName(string nameToSearch)
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE name = @SearchName;", DB.GetConnection());
      cmd.Parameters.Add(new SqlParameter("@SearchName", nameToSearch));

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Stylist> matches = new List<Stylist>{};
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int rating = rdr.GetInt32(2);

        Stylist newStylist = new Stylist(name, rating, id);
        matches.Add(newStylist);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();

      return matches;
    }

    public void Update(string newName, int newRating)
    {
      DB.CreateConnection();
      DB.OpenConnection();

      SqlCommand cmd = new SqlCommand("UPDATE stylists SET name = @StylistName, rating = @StylistRating OUTPUT INSERTED.name, INSERTED.rating WHERE id = @StylistId;", DB.GetConnection());

      cmd.Parameters.Add(new SqlParameter("@StylistName", newName));
      cmd.Parameters.Add(new SqlParameter("@StylistRating", newRating));
      cmd.Parameters.Add(new SqlParameter("@StylistId", this.GetId()));


      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
        this._rating = rdr.GetInt32(1);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      DB.CloseConnection();
    }
  }
}
