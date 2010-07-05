using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DynamicSQL;

namespace DynamicTest
{
    /// <summary>
    /// Require installation of Northwind sample database on local sql server
    /// </summary>
    [TestClass]
    public class DynamicSqlTests
    {
        private const string connectionString = "Data Source=(local);Initial Catalog=Northwind;Integrated Security=SSPI;";


        [TestMethod]
        public void SmokeTest()
        {
            var sql = new DynamicSql(connectionString);

            sql.EachRow("select * from Customers order by CustomerID",
                r => Console.WriteLine("{0} - {1}", r.CompanyName, r.ContactName));

        }


        [TestMethod]
        public void ShouldPerformActionToEachRow()
        {
            var sql = new DynamicSql(connectionString);

            sql.EachRow("select * from Customers order by CustomerID",
                r => Console.WriteLine("{0} - {1}", r.CompanyName, r.ContactName));

            decimal total = 0.0m;

            sql.EachRow("select * from [Order Details] where OrderID = 10251",
                r => total += r.Quantity * r.UnitPrice * (decimal)(1 - r.Discount));

            Assert.AreEqual(654.06m, total);
        }

        [TestMethod]
        public void ShouldPerformActionToFirstRow()
        {
            var sql = new DynamicSql(connectionString);

            string firstCustomer = "";

            sql.FirstRow("select * from Customers order by CustomerID", 
                r => firstCustomer = r.CustomerID);

            Assert.AreEqual("ALFKI", firstCustomer);
        }
    }
}
