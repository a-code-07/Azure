using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GarageProject.Models
{
    public class CarService
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Miles Cannot Contain Alphabetic Character")]
        public int Miles { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Price Cannot Contain Alphabetic Character")]
        public double Price { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public string ServiceType { get; set; }

        public int CarId { get; set; }

        [ForeignKey("CarId")]
        public Car Car { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateAdded { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Requested { get; set; }
    }
}