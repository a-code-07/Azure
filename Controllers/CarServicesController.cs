using GarageProject.Models;
using GarageProject.ViewModel;
using Microsoft.AspNet.Identity;
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
    public class CarServicesController : ApplicationBaseController
    {
        ApplicationDbContext db;

        public CarServicesController()
        {
            db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }



        public IEnumerable<ServiceRequest> RequestsList()
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("https://localhost:44346/api/");
                client.BaseAddress = new Uri("https://garageproject20190808114242.azurewebsites.net/api/");
                var responsetask = client.GetAsync("SerRequests");
                responsetask.Wait();

                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<ServiceRequest>>();
                    readTask.Wait();

                    return readTask.Result;

                }

            }
            return null;
        }

        public IEnumerable<Car> CarsList()
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("https://localhost:44346/api/");
                client.BaseAddress = new Uri("https://garageproject20190808114242.azurewebsites.net/api/");
                var responsetask = client.GetAsync("Cars");
                responsetask.Wait();

                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<Car>>();
                    readTask.Wait();

                    return readTask.Result;

                }

            }
            return null;
        }

        public IEnumerable<ApplicationUser> UserList()
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("https://localhost:44346/api/");
                client.BaseAddress = new Uri("https://garageproject20190808114242.azurewebsites.net/api/");
                var responsetask = client.GetAsync("Customers");
                responsetask.Wait();

                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<ApplicationUser>>();
                    readTask.Wait();

                    return readTask.Result;

                }

            }
            return null;
        }



        public ServiceRequest Getreq(int ? id)
        {
            string uri = "https://garageproject20190808114242.azurewebsites.net/api/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);

               
                var responseTask = client.GetAsync("SerRequests/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ServiceRequest>();
                    readTask.Wait();

                    return readTask.Result;
                }
            }
            return null;
        }

        public ActionResult AddServiceReq(ServiceRequest request)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("https://localhost:44346/api/cars");
                client.BaseAddress = new Uri("https://garageproject20190808114242.azurewebsites.net/api/cars");

                //HTTP POST
                JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();
                var postTask = client.PostAsync("SerRequests", request, formatter);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    if (HttpContext.User.IsInRole("admin"))
                        return RedirectToAction("Index", "User");
                    else
                        return RedirectToAction("IndexUser", "User");
                }
            }
            return View();
        }

        public IEnumerable<CarServicesDb> GetCarServicesDb()
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("https://localhost:44346/api/");
                client.BaseAddress = new Uri("https://garageproject20190808114242.azurewebsites.net/api/");
                var responsetask = client.GetAsync("Services");
                responsetask.Wait();

                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<CarServicesDb>>();
                    readTask.Wait();

                    return readTask.Result;
                    
                }
                
            }
            return null;
        }

    
        public ActionResult Create(Car car)
        {
            var viewModel = new CarAndServiceViewModel()
                {
                    CarServicesDbs=GetCarServicesDb(),
                    Cars=car                    
                };

                return View(viewModel);           
           
        }



        [HttpPost]
        public ActionResult Create(CarAndServiceViewModel viewModel)
        {
            //viewModel.ServiceCar.CarId = viewModel.Cars.Id;
            //viewModel.ServiceCar.DateAdded = DateTime.Today;

            var vM = new ServiceRequest()
            {
               CarId=viewModel.Cars.Id,
               DateRequested=DateTime.Today,
               Details=viewModel.ServiceCar.Details,
               Miles=viewModel.ServiceCar.Miles,
               Price=viewModel.ServiceCar.Price,
               ServiceType=viewModel.ServiceCar.ServiceType,
               UserId=User.Identity.GetUserId()
            };

            //db.ServiceRequests.Add(vM);
            //db.SaveChanges();

            AddServiceReq(vM);
            
            return RedirectToAction("Index","User");
        }
        
        public ActionResult Approve()
        {
            IEnumerable<ServiceRequest> requests = RequestsList();

            var vModel = new UserCarServiceReqViewModel()
            {
                Requests=requests,
                Cars=CarsList(),
                Users=UserList()
            };

            return View(vModel);
        }


        
        public ActionResult Approved(int ? id)
        {
            var sReq = Getreq(id);
            CarService cSer = new CarService()
            {
                //Car=db.Cars.Find(sReq.CarId),
                CarId=sReq.CarId,
                Requested=sReq.DateRequested,
                DateAdded=DateTime.Today,
                Details=sReq.Details,
                Miles=sReq.Miles,
                Price=sReq.Price,
                ServiceType=sReq.ServiceType               
            };

            //string uri = "https://localhost:44346/api/service";
            string uri = "https://garageproject20190808114242.azurewebsites.net/api/service";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();
                //HTTP POST
                var postTask = client.PostAsync<CarService>("ServicesCar", cSer, formatter);       //PutAsJsonAsync<Customer>("customers", customer);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var remSerReq = db.ServiceRequests.Find(id);
                    db.ServiceRequests.Remove(remSerReq);
                    db.SaveChanges();
                    return RedirectToAction("Approve");
                }
            }

            return View();
        }

       
        public ActionResult Decline(int ? id)
        {
            var ser=db.ServiceRequests.Find(id);
            db.ServiceRequests.Remove(ser);
            db.SaveChanges();
            return RedirectToAction("Approve");
        }




            // GET: CarServices
            public ActionResult ViewServices(Car car)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("https://localhost:44346/api/");
                client.BaseAddress = new Uri("https://garageproject20190808114242.azurewebsites.net/api/");
                var responsetask = client.GetAsync("servicescar?id="+car.Id);
                responsetask.Wait();

                var result = responsetask.Result;
                JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IEnumerable<CarService>>();
                    readTask.Wait();

                    var viewModel = new CarAndServiceViewModel()
                    {
                        CarServices=readTask.Result,
                        Cars=car,
                        PendingRequests=RequestsList()
                    };
                    return View(viewModel);
                }
            }
            return View();
        }


        public ActionResult Requests(ApplicationUser user)
        {
            return View();
        }
    }
}