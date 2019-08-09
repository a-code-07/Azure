using GarageProject.Models;
using GarageProject.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;

namespace GarageProject.Controllers
{
    [Authorize]
    public class UserController : ApplicationBaseController
    {
        ApplicationDbContext database;

        public UserController()
        {
            database = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            database.Dispose();
        }

        //Create User

        public ActionResult Create()
        {
            return View();
        }

    //    [HttpPost]
    //    public ActionResult Create(ApplicationUser user)
    //    {
    //        using (var client = new HttpClient())
    //        {
    //            client.BaseAddress = new Uri("https://localhost:44346/api/customers");

    //            //HTTP POST
    //            var postTask = client.PostAsJsonAsync("customers", user);
    //            postTask.Wait();

    //            var result = postTask.Result;
    //            if (result.IsSuccessStatusCode)
    //            {
    //                return RedirectToAction("Index");
    //            }
    //        }

    //        ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

    //        return View(user);
        
    //}




        // GET: User
        public ActionResult Index()
        {
            

           if(HttpContext.User.IsInRole("admin"))
            {
                //CustomerViewModel customer = null;
                using (var client = new HttpClient())
                {
                    //client.BaseAddress = new Uri("https://localhost:44346/api/");
                    client.BaseAddress = new Uri("https://garageproject20190808114242.azurewebsites.net/api/");
                    var responsetask = client.GetAsync("customers");
                    responsetask.Wait();

                    var result = responsetask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IEnumerable<ApplicationUser>>();
                        readTask.Wait();

                       var customer = readTask.Result;
                        return View(customer);
                    }
                    else
                    {


                        var customer = (CustomerViewModel)Enumerable.Empty<CustomerViewModel>();

                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }

                return View();
            }

            else
            {
               return RedirectToAction("IndexUser", "User");
            }


            //ApplicationDbContext database = new ApplicationDbContext();
            //// LoginViewModel userName = (LoginViewModel)TempData["myLoginData"];

            //string currentUserName = User.Identity.GetUserName();


            //CustomerViewModel customer = new CustomerViewModel()
            //{
            //    SingleUser= database.Users.Where(c => c.UserName.Equals(currentUserName)).SingleOrDefault()
           
            //};
           
           
        }

        public ActionResult IndexUser()
        {
            //string email = User.Identity.GetUserName();
            string uid = User.Identity.GetUserName();
            //string uri = "https://localhost:44346/api/";
            string uri = "https://garageproject20190808114242.azurewebsites.net/api/";


           
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);

                var viewModel1 = new CarAndCustomerViewModel();
                var responseTask = client.GetAsync("customers?username=" + uid.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ApplicationUser>();
                    readTask.Wait();

                    viewModel1.Users = readTask.Result;
                }
                
                var responseTask1 = client.GetAsync("cars");
                responseTask1.Wait();
                var result1 = responseTask1.Result;
                JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();
                if (result1.IsSuccessStatusCode)
                {
                    var readTask = result1.Content.ReadAsAsync<IEnumerable<Car>>();
                    readTask.Wait();
                    viewModel1.UserCar = readTask.Result.Where(c=>c.ApplicationUserId.Equals(viewModel1.Users.Id)).ToList();
                }
                viewModel1.CarBrandDbs = database.CarBrandDbs.ToList();
                viewModel1.CarStyleDbs = database.CarStyleDbs.ToList();
                return View(viewModel1);
            }  
        }

        //Edit Start Here
        
        public ActionResult EditUserPage(string email)
        {
           // ApplicationUser user = null;
            //string uri = "https://localhost:44346/api/";
            string uri = "https://garageproject20190808114242.azurewebsites.net/api/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);


                var responseTask = client.GetAsync("customers?username="+email);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ApplicationUser>();
                    readTask.Wait();

                    var customer = readTask.Result;

                    return View(customer);
                }
            }
            return View();
        }


        //public ActionResult EditData(ApplicationUser user)
        //{
        //    using(var db= new ApplicationDbContext())
        //    {
        //        var userData = db.Users.Where(c => c.UserName.Equals(user.UserName)).SingleOrDefault();

        //        userData.Name = user.Name;
        //        userData.PhoneNumber = user.PhoneNumber;
        //        userData.City = user.City;

        //        db.SaveChanges();

        //    }
        //    if (HttpContext.User.IsInRole("admin"))
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    else
        //        return RedirectToAction("IndexUser");
        //}

        //[HttpGet]
        //public ActionResult EditData(string email)
        //{
        //    ApplicationUser user = null;
        //    string uri = "https://localhost:44346/api/";
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(uri);


        //        var responseTask = client.GetAsync("customers?username=" + email);
        //        responseTask.Wait();

        //        var result = responseTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readTask = result.Content.ReadAsAsync<ApplicationUser>();
        //            readTask.Wait();

        //            user = readTask.Result;
        //        }
        //    }
        //    return View(user);           
        //}

        //    public bool IsValidate(ApplicationUser user)
        //{
        //    if(user.FirstName.)
        //    return true;
        //}

        [HttpPost]
        public ActionResult EditUserPage(ApplicationUser user)
        {
            //user.UserName = user.Email;
            //string uri = "https://localhost:44346/api/";
            string uri = "https://garageproject20190808114242.azurewebsites.net/api/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();
                //HTTP POST
                var putTask = client.PutAsync<ApplicationUser>("customers", user, formatter);       //PutAsJsonAsync<Customer>("customers", customer);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }


        ///Edit Ended
    }
}