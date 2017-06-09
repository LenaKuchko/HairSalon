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

    [Fact]
    public void TestStylist_SearchByName_ReturnsMatches()
    {
      Stylist stylist1 = new Stylist("Jessica", 5);
      stylist1.Save();
      Stylist stylist2 = new Stylist("Joan", 3);
      stylist2.Save();
      Stylist stylist3 = new Stylist("Rebecca", 4);
      stylist3.Save();
      Stylist stylist4 = new Stylist("John", 4);
      stylist4.Save();

      List<Stylist> controlList = new List<Stylist>{stylist4};
      List<Stylist> matches = Stylist.SearchByName("John");

      Assert.Equal(controlList, matches);
    }

    [Fact]
    public void TestStylist_Update_UpdatesStylist()
    {
      Stylist testStylist = new Stylist("Jessssssica", 1);
      testStylist.Save();
      testStylist.Update("Jessica", 5);
      Assert.Equal("Jessica", testStylist.GetName());
      Assert.Equal(5, testStylist.GetRating());
    }

    [Fact]
    public void TestStylist_GetClients_GetsClientsByStylist()
    {
      Stylist newStylist = new Stylist("Jessica", 5);
      newStylist.Save();

      Client client1 = new Client("Rebecca",newStylist.GetId());
      rest1.Save();
      Client client2 = new Client("Tom", newStylist.GetId());
      rest2.Save();


      List<Client> controlList = new List<Client>{client1, client2};
      List<Client> testList = newCuisine.GetClients();

      Assert.Equal(controlList, testList);
    }

   public void Dispose()
   {
     Stylist.DeleteAll();
   }
  }
}
