using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoodDatabase;

namespace FoodServices.Controllers
{
    public class UserServicesController : ApiController
    {

        public List<fs_user> GetRegUsers()
        {
            using (FoodServicesEntities entities = new FoodServicesEntities())
            {
                return entities.fs_user.ToList();
            }
        }

        public fs_user GetRegUsers(int id)
        {
            using (FoodServicesEntities entities = new FoodServicesEntities())
            {
                var user = entities.fs_user.Find(id);
                return user;
            }
        }

        public void Post(fs_user temp)
        {
            using (FoodServicesEntities entities = new FoodServicesEntities())
            {
                entities.fs_user.Add(temp);
                entities.SaveChanges();
            }
        }

        [HttpPut]
        public void Put(int id, fs_user temp)
        {
            using (FoodServicesEntities entities = new FoodServicesEntities())
            {
                entities.Entry(temp).State = System.Data.Entity.EntityState.Modified;
                entities.SaveChanges();
            }
        }

        [HttpDelete]
        public void Delete(int id)
        {
            using (FoodServicesEntities entities = new FoodServicesEntities())
            {
                var user = entities.fs_user.Find(id);
                entities.fs_user.Remove(user);
                entities.SaveChanges();
            }
        }
    }
}
