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

    public static void DeleteAllStylists()
    {
      // DB.Connection
    }
  }
}
