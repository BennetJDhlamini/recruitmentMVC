using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace recruitmentMVC.ViewModels
{
    public class AddApplicationViewModel
    {
        [Required, MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        [Display(Name = "Full name(s)")]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Contact { get; set; }
        public IFormFile Resume { get; set; }
        public IFormFile ID { get; set; }
        public IFormFile Transcript { get; set; }
        public IFormFile matricCertificate { get; set; }
        public List<IFormFile> relevantDocs { get; set; }
    }
}
