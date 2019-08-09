using GarageProject.Models;
using GarageProject.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace GarageProject.Controllers
{
    [Authorize]
    public class VehicleController : Controller
    {
        ApplicationDbContext db;

        public VehicleController()
        {
            db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }


        // GET: Vehicle
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult ViewCar(ApplicationUser user)
        //{
        //    var viewModel = new CustomerViewModel()
        //    {
        //        SingleUser = user,
        //        Cars = db.Cars.ToList()
        //    };
        //    return View(viewModel);
        //}

        public ActionResult Create(ApplicationUser user)
        {

            var viewModel = new CarAndCustomerViewModel()
            {
                Users=user,
                CarBrandDbs=db.CarBrandDbs.ToList(),
                CarStyleDbs=db.CarStyleDbs.ToList()
            };
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult Create(CarAndCustomerViewModel viewModel)
        {
            viewModel.Cars.ApplicationUserId = viewModel.Users.Id;
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
               
                var car = viewModel.Cars;
                var user = db.Users.Find(viewModel.Users.Id);
                using (var client = new HttpClient())
                {
                    //client.BaseAddress = new Uri("https://localhost:44346/api/cars");
                    client.BaseAddress = new Uri("https://garageproject20190808114242.azurewebsites.net/api/cars");

                    //HTTP POST
                    JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();
                    var postTask = client.PostAsync("cars", car, formatter);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        if (HttpContext.User.IsInRole("admin"))
                            return RedirectToAction("ViewCar","Vehicle" ,user);
                        else
                            return RedirectToAction("ViewCar", "Vehicle",user);
                    }
                }
            }


            // ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View();


        }

        public ActionResult ViewCar(ApplicationUser user)
        {
            //IEnumerable<Car> car = null;
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("https://localhost:44346/api/");
                client.BaseAddress = new Uri("https://garageproject20190808114242.azurewebsites.net/api/");
                var responsetask = client.GetAsync("cars");
                responsetask.Wait();

                var result = responsetask.Result;
                JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<Car>>();
                    readTask.Wait();

                    var viewModel = new ViewCarCustomerViewModel()
                         {
                            Users = user,
                             Cars = readTask.Result
                         };
                    return View(viewModel);
                }
            }
            //var viewModel = new CustomerViewModel()
            //{
            //    SingleUser = user,
            //    Cars = car
            //};
            return View(user);
        }

        public ActionResult EditCar(Car car)
        {
            // ApplicationUser user = null;
            //string uri = "https://localhost:44346/api/";
            string uri = "https://garageproject20190808114242.azurewebsites.net/api/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);


                var responseTask = client.GetAsync("cars?id=" +car.Id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Car>();
                    readTask.Wait();

                    var viewModel = new CarAndCustomerViewModel()
                    {
                        Cars=readTask.Result,
                        Users=db.Users.Where(c=>c.Id.Equals(car.ApplicationUserId)).SingleOrDefault(),
                        CarBrandDbs=db.CarBrandDbs.ToList(),
                        CarStyleDbs=db.CarStyleDbs.ToList()
                    };

                    return View(viewModel);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditCar(CarAndCustomerViewModel car)
        {
            var user = db.Users.Find(car.Users.Id);
            //string uri = "https://localhost:44346/api/";
            string uri = "https://garageproject20190808114242.azurewebsites.net/api/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();
                //HTTP POST
                var putTask = client.PutAsync<Car>("cars", car.Cars, formatter);       //PutAsJsonAsync<Customer>("customers", customer);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("ViewCar",user);
                }
            }
            return View();
        }

    }
}