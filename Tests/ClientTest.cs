using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HairSalon.Objects;

namespace HairSalon
{
  [Collection("HairSalon")]

  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=DESKTOP-6CVACGR\\SQLEXPRESS;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void TestClient_Equal_ReturnEqualValues()
    {
      Stylist newStylist = new Stylist("Jessica", 5);
      newStylist.Save();
      Client newClient = new Client("Tom", newStylist.GetId());
      Client testClient = new Client("Tom", newStylist.GetId());
      Assert.Equal(newClient, testClient);
    }

    [Fact]
    public void TestClient_DatabaseEmptyAtFirst()
    {
      List<Client> allClients = new List<Client>{};
      List<Client> testList = Client.GetAll();
      Assert.Equal(allClients, testList);
    }

    [Fact]
    public void TestClient_Save_ClientToDatabase()
    {
     Stylist newStylist = new Stylist("Jessica", 5);
     newStylist.Save();
     Client newClient = new Client("Tom", newStylist.GetId());
     newClient.Save();
     Client savedClient = Client.GetAll()[0];
     Assert.Equal(newClient, savedClient);
    }

    [Fact]
    public void TestClient_Find_ClientInDatabase()
    {
      Stylist newStylist = new Stylist("Jessica", 5);
      newStylist.Save();
      Client newClient = new Client("Tom", newStylist.GetId());
      newClient.Save();
      Client foundClient = Client.Find(newClient.GetId());
      Assert.Equal(newClient, foundClient);
    }

    [Fact]
    public void TestClient_Delete_SingleClient()
    {
      Stylist newStylist = new Stylist("Jessica", 5);
      newStylist.Save();
      Client newClient1 = new Client("Tom", newStylist.GetId());
      newClient1.Save();
      Client newClient2 = new Client("Joan", newStylist.GetId());
      newClient2.Save();

      newClient1.DeleteSingleClient();

      List<Client> controlList = new List<Client>{newClient2};
      List<Client> testList = Client.GetAll();

      Assert.Equal(controlList, testList);
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}
