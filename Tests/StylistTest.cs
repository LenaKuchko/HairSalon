using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HairSalon.Objects;

namespace HairSalon
{
  [Collection("HairSalon")]

  public class StylistTest
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void TestStylist_DatabaseEmptyAtFirst()
    {
      //Arrange
      List<Stylist> allStylists = new List<Stylist>{};

      //Act
      List<Stylist> testList = Stylist.GetAll();

      //Assert
      Assert.Equal(allStylists, testList);
    }
  }
}
