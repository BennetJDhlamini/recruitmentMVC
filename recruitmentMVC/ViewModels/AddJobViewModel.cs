using Microsoft.AspNetCore.Http;
using recruitmentMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace recruitmentMVC.ViewModels
{
    public class AddJobViewModel
    {
        [Required, MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        [Display(Name = "Company Name")]
        public string Name { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        [Display(Name = "Job Type")]
        public Typ? Type { get; set; }
        [Required]
        public int Salary { get; set; }
        [Required]
        public Department? Department { get; set; }
        [Required]
        public string Qualification { get; set; }
        [Required]
        public string Skills { get; set; }
        [Required]
        public string Experience { get; set; }
        [Required]
        public string About { get; set; }
        [Required]
        [Display(Name = "Date posted")]
        public string pDate { get; set; }
        [Required]
        [Display(Name = "Closing Date")]
        public string cDate { get; set; }
        public IFormFile Photo { get; set; }
    }
}
