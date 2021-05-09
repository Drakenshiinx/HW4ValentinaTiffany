using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TiffanyValentinaHW4_ef.Controllers
{
    public class Query2And3Controller : ApiController
    {
        NodeOrders500Entities myDB = new NodeOrders500Entities();

        //constructor for the employee table
        public class EmployeeTableClass
        {
            public EmployeeTableClass()
            {

            }

            public EmployeeTableClass(int id, string empname)
            {
                SalesPersonID = id;
                EmployeeFirstLastName = empname;
            }

            public int SalesPersonID { get; set; }
            public string EmployeeFirstLastName { get; set; }
        }

        //constructor for the store locations
        public class StoreTableClass
        {
            public StoreTableClass()
            {

            }

            public StoreTableClass(int id, string city)
            {
                StoreID = id;
                City = city;
            }

            public int StoreID { get; set; }
            public string City { get; set; }
        }

        //this populates the employees first and last names into a drop down menu
        [HttpGet]
        [ActionName("EmployeeNames")]

        public IEnumerable<EmployeeTableClass> GetEmployeeNames()
        {
            var EmployeeNames = from x in myDB.SalesPersonTables
                                select new EmployeeTableClass
                                { SalesPersonID = x.salesPersonID, EmployeeFirstLastName = x.FirstName + " " + x.LastName };

            return EmployeeNames.Distinct().ToList();
        }

        //this adds the total number of sales sold for the whole year by that employee
        [ActionName("EmployeeSales")]
        public IHttpActionResult GetEmployeeSales(int id)
        {
            int empSales = 0;

            try
            {
                empSales = (from x in myDB.Orders
                         where x.salesPersonID == id && x.dayPurch <= 365
                         select x.pricePaid).Sum();
            }
            catch
            {
                return Ok(0);
            }
            return Ok(empSales);
        }
        //This creates the names of the cities the stores are located in the drop down menu.  
        [ActionName("StoreCity")]
        public IEnumerable<StoreTableClass> GetStoresCity()
        {
            var cityLocation = from x in myDB.StoreTables
                               select new StoreTableClass
                               {
                                   StoreID = x.storeID,
                                   City = x.City
                               };

            return cityLocation.Distinct().ToList();
        }

        //This adds the total number of sales sold for the whole year at a specific store.  
        [ActionName("StoreSales")]
        public IHttpActionResult GetSalesOfStore(int id)
        {
            int storeSales = 0;

            try
            {
                storeSales = (from x in myDB.Orders
                         where x.storeID == id && x.dayPurch <= 365
                         select x.pricePaid).Sum();

            }
            catch
            {

                return Ok(0);
            }

            return Ok(storeSales);
        }
    }
}
