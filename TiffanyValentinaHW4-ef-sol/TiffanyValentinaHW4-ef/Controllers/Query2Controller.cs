using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TiffanyValentinaHW4_ef.Controllers
{
    public class Query2Controller : ApiController
    {
        NodeOrders500Entities myDB = new NodeOrders500Entities();

        //constructor for employee table class
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

        //This populates the employees first and last names into the drop down menu
        [HttpGet]
        [ActionName("EmployeeNames")]

        public IEnumerable<EmployeeTableClass> GetEmployeeNames()
        {
            var employeeNames = from x in myDB.SalesPersonTables
                                select new EmployeeTableClass
                                { SalesPersonID = x.salesPersonID, EmployeeFirstLastName = x.FirstName + " " + x.LastName };

            return employeeNames.Distinct().ToList();
        }

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
    }
}
