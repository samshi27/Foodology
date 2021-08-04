using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoodDatabase;

namespace FoodServices.Controllers
{
    public class AdminServicesController : ApiController
    {
        [HttpPut]
        public void Put(int id, fs_restaurant temp)
        {
            using (FoodDatabaseEntities entities = new FoodDatabaseEntities())
            {
                entities.Entry(temp).State = System.Data.Entity.EntityState.Modified;
                entities.SaveChanges();
            }
        }
    }
}
