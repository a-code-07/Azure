using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GarageProject.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public string VIN { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Style { get; set; }

        [Required]
        [RegularExpression(@"([A-Z][a-z]*)([\\s\\\'-][A-Z][a-z]*)*", ErrorMessage = "Enter valid Color Name")]
        public string Color { get; set; }



        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}