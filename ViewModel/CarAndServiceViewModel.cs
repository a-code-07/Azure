using GarageProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GarageProject.ViewModel
{
    public class CarAndServiceViewModel
    {
        public Car Cars { get; set; }
        public CarService ServiceCar { get; set; }
        public IEnumerable<CarService> CarServices { get; set; }
        public IEnumerable<CarServicesDb> CarServicesDbs { get; set; }
        public IEnumerable<ServiceRequest> PendingRequests { get; set; }
    }
}