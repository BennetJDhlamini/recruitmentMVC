using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace recruitmentMVC.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Full name(s) cannot exceed 50 characters")]
        [Display(Name = "Full name(s)")]
        public string Name { get; set; }
        [Required, MaxLength(20, ErrorMessage = "Surname cannot exceed 50 characters")]
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Contact { get; set; }
        public string ResumePath { get; set; }
        public string IdPath { get; set; }
        public string transcriptPath { get; set; }
        public string matricCertificatePath { get; set; }
        public string relevantDocsPath { get; set; }
    }
}
