using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HairSalon.Objects;

namespace HairSalon
{
  [Collection("HairSalon")]

  public class ClientTest
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
  }
}
