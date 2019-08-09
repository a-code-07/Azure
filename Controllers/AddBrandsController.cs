using GarageProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GarageProject.Controllers
{
    public class AddBrandsController : ApiController
    {
        readonly ApplicationDbContext db;

        public AddBrandsController()
        {
            db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        public IHttpActionResult GetAllBrands()
        {
            return Ok(db.CarBrandDbs.ToList());
        }

        [HttpPost]
        public void AddServices(CarBrandDb brand)
        {
            //CarBrandDb dbt = new CarBrandDb()
            //{
            //    CarBrandsData = name
            //};
            db.CarBrandDbs.Add(brand);
            db.SaveChanges();
           
        }

        [HttpDelete]
        public void DeleteBrand(int id)
        {
            var brnd = db.CarBrandDbs.Find(id);
            db.CarBrandDbs.Remove(brnd);
            db.SaveChanges();
        }
    }
}
