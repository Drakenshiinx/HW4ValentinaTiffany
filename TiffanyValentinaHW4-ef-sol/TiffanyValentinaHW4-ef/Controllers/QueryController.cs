using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TiffanyValentinaHW4_ef.Controllers
{
    public class QueryController : ApiController
    {

        NodeOrders500Entities7 myDB = new NodeOrders500Entities7();


        //constructor for query 1
        public class CDs
        {
            public CDs(string pCity, int pCount)
            {
                City = pCity;
                Count = pCount;
            }
            public string City { get; set; }
            public int Count { get; set; }
        }

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

        //query 1
        [HttpGet]
        [ActionName("StoresNames")]
        public IEnumerable<Order> GetAllOrders()
        {
            return myDB.Orders;
        }
        public IEnumerable<StoreTable> GetAllStores()
        {
            return myDB.StoreTables;
        }

        public IHttpActionResult GetShowData(int x)
        {

            var query1 = from eachStore in myDB.StoreTables
                         where eachStore.storeID == x
                         group eachStore by eachStore.City;

            List<CDs> myList = new List<CDs>();

            foreach (var group in query1)
            {
                myList.Add(new CDs(group.Key, group.Count()));
            }

            return Json(myList);
        }

        //this populates the employees first and last names into a drop down menu
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
