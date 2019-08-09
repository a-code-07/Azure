using GarageProject.Models;
using GarageProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GarageProject.APIs
{
    public class CustomersController : ApiController
    {
        ApplicationDbContext database;

        public CustomersController()
        {
            database = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            database.Dispose();
        }

        [HttpGet]
        public IHttpActionResult GetAllCustomers()
        {

            var customer = database.Users.ToList();
        


            if(customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpGet]
        public IHttpActionResult GetCustomerById(string username )
        {
            // CustomerViewModel customer = null;
            var customer = database.Users.Where(c => c.UserName.ToLower().Equals(username.ToLower().ToString())).SingleOrDefault();
            return Ok(customer);
        }


        [HttpPost]
        public IHttpActionResult AddCustomer(ApplicationUser customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Users.Add(customer);

                ctx.SaveChanges();
            }

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult UpdateDetails(ApplicationUser  customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new ApplicationDbContext())
            {
                var existingStudent = ctx.Users.Where(s => s.UserName.Equals(customer.UserName))
                                                        .FirstOrDefault<ApplicationUser>();

                if (existingStudent != null)
                {
                    existingStudent.FirstName = customer.FirstName;
                    existingStudent.LastName = customer.LastName;
                    existingStudent.City = customer.City;
                    existingStudent.PhoneNumber = customer.PhoneNumber;                  

                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(string  id)
        {
            if (id==null)
                return BadRequest("Not a valid customer");

            var student = database.Users.Find(id);

            //var student = database.Users
            //    .Where(s => s.Id.Equals(id))
            //    .FirstOrDefault();

            //database.Entry(student).State = System.Data.Entity.EntityState.Deleted;
            database.Users.Remove(student);
                database.SaveChanges();
            

            return Ok();
        }
    }
}
