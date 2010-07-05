using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DynamicSQL;

namespace DynamicTest
{
    /// <summary>
    /// Summary description for CoolSqlTests
    /// </summary>
    [TestClass]
    public class ExpandoSqlTests
    {
        private const string connectionString = "Data Source=(local);Initial Catalog=Northwind;Integrated Security=SSPI;";


        [TestMethod]
        public void SmokeTest()
        {
            var sql = new ExpandoSql(connectionString);

            sql.EachRow("select * from Customers order by CustomerID",
                r => Console.WriteLine("{0} - {1}", r.CompanyName, r.ContactName));

        }


        [TestMethod]
        public void ShouldPerformActionToEachRow()
        {
            var sql = new ExpandoSql(connectionString);

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
            var sql = new ExpandoSql(connectionString);

            string firstCustomer = "";

            sql.FirstRow("select * from Customers order by CustomerID", 
                r => firstCustomer = r.CustomerID);

            Assert.AreEqual("ALFKI", firstCustomer);
        }
    }
}
