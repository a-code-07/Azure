using GarageProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GarageProject.ViewModel
{
    public class CustomerViewModel
    {
        public IEnumerable<ApplicationUser> Customer { get; set; }
        public ApplicationUser SingleUser { get; set; }
        public string UserName { get; set; }
        public IEnumerable<Car> Cars { get; set; }
    }
}