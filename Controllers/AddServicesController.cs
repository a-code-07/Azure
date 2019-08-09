using GarageProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GarageProject.Controllers
{
    public class AddServicesController : ApiController
    {
        readonly ApplicationDbContext db;

        public AddServicesController()
        {
            db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        //public IHttpActionResult GetAllServices()
        //{
        //    return Ok(db.CarServicesDbs.ToList());
        //}

        public IHttpActionResult GetId(int i=0)
        {
            if (i == 0)
                return Ok(db.CarServicesDbs.ToList().LastOrDefault().Id);
            else
                return Ok();
        }

        [HttpPost]
        public void AddServices(CarServicesDb service)
        {       
            db.CarServicesDbs.Add(service);
            db.SaveChanges();           
        }

        [HttpDelete]
        public void DeleteService(int id)
        {
            var service = db.CarServicesDbs.Find(id);
            db.CarServicesDbs.Remove(service);
            db.SaveChanges();
        }
    }
}
