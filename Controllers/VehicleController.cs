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
    public class VehicleController : ApplicationBaseController
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

       

        public IEnumerable<CarStyleDb> CarStyleList()
        {
            string uri = "https://garageproject20190808114242.azurewebsites.net/api/";

            using (var styleClient = new HttpClient())
            {
                styleClient.BaseAddress = new Uri(uri);

                var responseTaskStyle = styleClient.GetAsync("AddStyles");
                responseTaskStyle.Wait();
                var resultStyl = responseTaskStyle.Result;

                if (resultStyl.IsSuccessStatusCode)
                {
                    var readStyle = resultStyl.Content.ReadAsAsync<IEnumerable<CarStyleDb>>();
                    readStyle.Wait();

                    return readStyle.Result;
                }
                //viewModel1.CarBrandDbs = database.CarBrandDbs.ToList();
                // viewModel1.CarStyleDbs = database.CarStyleDbs.ToList();
                return null;
            }
        }


        public  IEnumerable<CarBrandDb> CarBrandList()
        {
            string uri = "https://garageproject20190808114242.azurewebsites.net/api/";

            using (var brndClient = new HttpClient())
            {
                brndClient.BaseAddress = new Uri(uri);

                var responseTaskBrnd = brndClient.GetAsync("AddBrands");
                responseTaskBrnd.Wait();
                var resultBrnd = responseTaskBrnd.Result;

                if (resultBrnd.IsSuccessStatusCode)
                {
                    var readbrnd = resultBrnd.Content.ReadAsAsync<IEnumerable<CarBrandDb>>();
                    readbrnd.Wait();

                    return readbrnd.Result;
                }

            }
            return null;

        }

        public ApplicationUser GetUsrById(string uid)
        {
            string uri = "https://garageproject20190808114242.azurewebsites.net/api/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);

                var viewModel1 = new CarAndCustomerViewModel();
                var responseTask = client.GetAsync("customers?id=" + uid.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ApplicationUser>();
                    readTask.Wait();

                    return readTask.Result;
                }
            }
            return null;
        }

        public ApplicationUser GetUsr(string uid)
        {
            string uri = "https://garageproject20190808114242.azurewebsites.net/api/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);

                var viewModel1 = new CarAndCustomerViewModel();
                var responseTask = client.GetAsync("customers/" + uid.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ApplicationUser>();
                    readTask.Wait();

                    return readTask.Result;
                }
            }
            return null;
        }

        public ActionResult Create(ApplicationUser user)
        {
            

            var viewModel = new CarAndCustomerViewModel()
            {
                Users = user,
                CarBrandDbs = CarBrandList(),
                CarStyleDbs=  CarStyleList()
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
                var user = GetUsr(viewModel.Users.Id);
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
                            return RedirectToAction("IndexUser", "User");
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
                        Users=GetUsrById(car.ApplicationUserId),
                        CarBrandDbs=CarBrandList(),
                        CarStyleDbs=CarStyleList()
                    };

                    return View(viewModel);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditCar(CarAndCustomerViewModel car)
        {
            var user = GetUsrById(car.Users.Id);
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