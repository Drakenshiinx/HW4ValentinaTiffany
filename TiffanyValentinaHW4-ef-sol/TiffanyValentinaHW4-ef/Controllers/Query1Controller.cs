using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TiffanyValentinaHW4_ef.Controllers
{
    public class Query1Controller : ApiController
    {
        NodeOrders500Entities5 myDB = new NodeOrders500Entities5();
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
        //public IHttpActionResult GetOrder(int id)
        //{
        //    Order order = myDB.Orders.FirstOrDefault((p) => p.storeID == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(order);
        //}
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
    }
}
