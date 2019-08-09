using GarageProject.Models;
using GarageProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GarageProject.Controllers
{
    public class CarsController : ApiController
    {
        ApplicationDbContext database;

        public CarsController()
        {
            database = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            database.Dispose();
        }

        [HttpGet]
        public IHttpActionResult GetAllCars()
        {

            var cars = database.Cars.ToList();
            

            if(cars== null)
            {
                return NotFound();
            }

            return Ok(cars);
        }

        [HttpGet]
        public IHttpActionResult GetCarById(int ? id)
        {
            // CustomerViewModel customer = null;
            var cars = database.Cars.Find(id);
            return Ok(cars);
        }

        [HttpPost]
        public void AddCar(Car car)
        {
            if (!ModelState.IsValid)
                 BadRequest("Invalid data.");
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Cars.Add(car);

                ctx.SaveChanges();
            }

        }


        [HttpPut]
        public IHttpActionResult UpdateCarDetails(Car car)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");


            var existingCar = database.Cars.Find(car.Id);

                if (existingCar != null)
                {
                    existingCar.VIN = car.VIN;
                    existingCar.Model = car.Model;
                    existingCar.Style = car.Style;
                existingCar.Brand = car.Brand;

                    database.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int ? id)
        {
            if (id == 0)
                return BadRequest("Not a valid customer");


            var car = database.Cars.Find(id);

            database.Entry(car).State = System.Data.Entity.EntityState.Deleted;
            database.SaveChanges();


            return Ok();
        }
    }
}
