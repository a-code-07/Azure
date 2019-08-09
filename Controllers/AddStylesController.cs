using GarageProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GarageProject.Controllers
{
    public class AddStylesController : ApiController
    {
        readonly ApplicationDbContext db;

        public AddStylesController()
        {
            db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        public IHttpActionResult GetAllStyles()
        {
            return Ok(db.CarStyleDbs.ToList());
        }

        [HttpPost]
        public void AddStyles(CarStyleDb style)
        {
           
            db.CarStyleDbs.Add(style);
            db.SaveChanges();

        }

        [HttpDelete]
        public void DeleteStyle(int id)
        {
            var style = db.CarStyleDbs.Find(id);
            db.CarStyleDbs.Remove(style);
            db.SaveChanges();
        }

    }
}
