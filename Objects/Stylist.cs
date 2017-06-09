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
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);
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
      DB.CloseConnection(conn);

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
  }
}
