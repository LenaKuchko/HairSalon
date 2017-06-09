using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HairSalon.Objects;

namespace HairSalon
{
  [Collection("HairSalon")]

  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=DESKTOP-6CVACGR\\SQLEXPRESS;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void TestStylist_DatabaseEmptyAtFirst()
    {
      List<Stylist> allStylists = new List<Stylist>{};
      List<Stylist> testList = Stylist.GetAll();
      Assert.Equal(allStylists, testList);
    }

    [Fact]
    public void TestStylist_Equal_ReturnEqualValues()
    {
      Stylist newStylist = new Stylist("Jessica", 5);
      Stylist testStylist = new Stylist("Jessica", 5);
      Assert.Equal(newStylist, testStylist);
    }

    [Fact]
    public void TestStylist_Save_StylistToDatabase()
    {
     Stylist newStylist = new Stylist("Jessica", 5);
     newStylist.Save();
     Stylist savedStylist = Stylist.GetAll()[0];
     Assert.Equal(newStylist, savedStylist);
    }

    [Fact]
    public void TestStylist_Find_StylistInDatabase()
    {
      Stylist newStylist = new Stylist("Jessica", 5);
      newStylist.Save();
      Stylist foundStylist = Stylist.Find(newStylist.GetId());
      Assert.Equal(newStylist, foundStylist);
    }

    [Fact]
    public void TestStylist_Delete_SingleStylist()
    {
      Stylist newStylist1 = new Stylist("Jessica", 5);
      newStylist1.Save();
      Stylist newStylist2 = new Stylist("Joan", 3);
      newStylist2.Save();

      newStylist1.DeleteSingleStylist();

      List<Stylist> controlList = new List<Stylist>{newStylist2};
      List<Stylist> testList = Stylist.GetAll();

      Assert.Equal(controlList, testList);
    }

   public void Dispose()
   {
     Stylist.DeleteAll();
   }
  }
}
