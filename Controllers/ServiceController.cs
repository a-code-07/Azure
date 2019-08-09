using GarageProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GarageProject.Controllers
{
    public class ServiceController : ApiController
    {

        ApplicationDbContext db;

        public ServiceController()
        {
            db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }


        //[HttpGet]
        //public IHttpActionResult GetCustomerById(int ? id)
        //{
        //    // CustomerViewModel customer = null;
        //    var service = db.CarServices.Where(c => c.CarId==id).ToList();
        //    return Ok(service);
        //}

        [HttpGet]
        public IHttpActionResult GetServiceById(int ? id)
        {
            var service = db.CarServices.Where(c => c.CarId == id).ToList();

            return Ok(service);
        }

        [HttpPost]
        public void AddService(CarService service)
        {
            if (!ModelState.IsValid)
                BadRequest("Invalid data.");
            using (var ctx = new ApplicationDbContext())
            {
                ctx.CarServices.Add(service);

                ctx.SaveChanges();
            }

        }

        [HttpDelete]
        public IHttpActionResult Delete(int? id)
        {
            if (id == 0)
                return BadRequest("Not a valid customer");


            var service = db.CarServices.Find(id);

            db.Entry(service).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();


            return Ok();
        }
    }
}
