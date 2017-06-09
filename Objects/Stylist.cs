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

      return null;
    }
  }
}
