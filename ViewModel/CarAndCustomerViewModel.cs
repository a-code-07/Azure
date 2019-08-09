using GarageProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GarageProject.ViewModel
{
    public class CarAndCustomerViewModel
    {
        public ApplicationUser Users { get; set; }
        public Car Cars { get; set; }
        public IEnumerable<Car> UserCar { get; set; }
        public IEnumerable<CarStyleDb> CarStyleDbs { get; set; }
        public IEnumerable<CarBrandDb> CarBrandDbs { get; set; }
    }
}