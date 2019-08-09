using GarageProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GarageProject.ViewModel
{
    public class AdminAddViewModel
    {
        public IEnumerable<CarServicesDb> ShowServiceTypes { get; set; }
        public IEnumerable<CarStyleDb> ShowCarStyle { get; set; }
        public IEnumerable<CarBrandDb> ShowCarBrand { get; set; }
        public CarServicesDb AddServices { get; set; }
        public CarStyleDb AddCarStyles { get; set; }
        public CarBrandDb AddCarBrands { get; set; }
    }
}