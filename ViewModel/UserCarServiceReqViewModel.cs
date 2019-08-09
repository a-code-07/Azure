using GarageProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GarageProject.ViewModel
{
    public class UserCarServiceReqViewModel
    {
        public IEnumerable<ServiceRequest> Requests { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<Car> Cars { get; set; }
    }
}