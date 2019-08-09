using GarageProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GarageProject.ViewModel
{
    public class ViewCarCustomerViewModel
    {
        public ApplicationUser Users { get; set; }
        public IEnumerable<Car> Cars { get; set; }
    }
}