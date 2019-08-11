using GarageProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GarageProject.Controllers
{
    public class SerRequestsController : ApiController
    {
        ApplicationDbContext db;

        public SerRequestsController()
        {
            db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        public IHttpActionResult GetAllSerRequest()
        {
            return Ok(db.ServiceRequests.ToList());
        }

        [HttpGet]
        public IHttpActionResult GetAllSerRequestById(int ? id)
        {
            return Ok(db.ServiceRequests.Find(id));
        }

        [HttpPost]
        public IHttpActionResult AddRequest(ServiceRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            else
            {
                db.ServiceRequests.Add(request);
                db.SaveChanges();
                return Ok();
            }
           
        }

        [HttpDelete]
        public IHttpActionResult DelSerRequest(int ? id)
        {
            var req = db.ServiceRequests.Find(id);
            db.ServiceRequests.Remove(req);
            db.SaveChanges();
            return Ok();
        }
    }
}
