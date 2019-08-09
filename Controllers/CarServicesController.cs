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
    public class CarServicesController : Controller
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

    
        public ActionResult Create(Car car)
        {
            var viewModel = new CarAndServiceViewModel()
                {
                    CarServicesDbs=db.CarServicesDbs.ToList(),
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

            db.ServiceRequests.Add(vM);
            db.SaveChanges();


            
            return RedirectToAction("Index","User");
        }
        
        public ActionResult Approve()
        {
            IEnumerable<ServiceRequest> requests = db.ServiceRequests.ToList();

            var vModel = new UserCarServiceReqViewModel()
            {
                Requests=requests,
                Cars=db.Cars.ToList(),
                Users=db.Users.ToList()
            };

            return View(vModel);
        }


        
        public ActionResult Approved(int ? id)
        {
            var sReq = db.ServiceRequests.Find(id);
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
                var postTask = client.PostAsync<CarService>("service", cSer, formatter);       //PutAsJsonAsync<Customer>("customers", customer);
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
                var responsetask = client.GetAsync("service?id="+car.Id);
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
                        PendingRequests=db.ServiceRequests.ToList()
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