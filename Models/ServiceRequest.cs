using GarageProject.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GarageProject.Models
{
    public class ServiceRequest
    {
        public int Id { get; set; }
        public string UserId { get; set; }  
        public int Miles { get; set; }     
        public double Price { get; set; }   
        public string Details { get; set; }      
        public string ServiceType { get; set; }     
        public int CarId { get; set; }    

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime  DateRequested { get; set; }
    }
}