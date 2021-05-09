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
        NodeOrders500Entities2 myDB = new NodeOrders500Entities2();

        public class EmployeeTableClass
        {
            public EmployeeTableClass()
            {

            }

            public EmployeeTableClass(int id, string name)
            {
                SalesPersonID = id;
                EmployeeFirstLastName = name;
            }

            public int SalesPersonID { get; set; }
            public string EmployeeFirstLastName { get; set; }
        }

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


        [HttpGet]
        [ActionName("EmployeeNames")]

        public IEnumerable<EmployeeTableClass> GetEmployeeNames()
        {
            var EmployeeNames = from x in myDB.SalesPersonTables
                                    //join y in myDB.Orders on x.salesPersonID equals y.salesPersonID
                                    //where y.pricePaid > 0
                                select new EmployeeTableClass
                                { SalesPersonID = x.salesPersonID, EmployeeFirstLastName = x.FirstName + " " + x.LastName };

            return EmployeeNames.Distinct().ToList();
        }

        [ActionName("EmployeeSales")]
        public IHttpActionResult GetEmployeeSales(int id)
        {

            int sales = 0;
            try
            {
                sales = (from x in myDB.Orders
                         where x.salesPersonID == id && x.dayPurch <= 365
                         select x.pricePaid).Sum();
            }
            catch
            {
                return Ok(0);
            }
            return Ok(sales);
        }

        [ActionName("StoreCity")]
        public IEnumerable<StoreTableClass> GetStoresCity()
        {
            var cityLocation = from x in myDB.StoreTables
                                   //join y in myDB.Orders on x.storeID equals y.storeID
                                   //where y.pricePaid > 0
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
            int sales = 0;
            try
            {
                sales = (from x in myDB.Orders
                         where x.storeID == id && x.dayPurch <= 365
                         select x.pricePaid).Sum();

            }
            catch
            {

                return Ok(0);
            }

            return Ok(sales);
        }
    }
}
