using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using recruitmentMVC.Models;
using recruitmentMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace recruitmentMVC.Controllers
{
    public class HomeController : Controller
    {
        public IJobRepository _jobRepository;
        public IApplicationRepository _applicationRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        [BindProperty(SupportsGet = true)]
        public string searchJob { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchApplication { get; set; }

        public HomeController(IApplicationRepository applicationRepository, IJobRepository jobRepository, IWebHostEnvironment hostingEnvironment)
        {
            _jobRepository = jobRepository;
            _applicationRepository = applicationRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        [AllowAnonymous]
        [HttpGet]
        public ViewResult Index()
        {
            var model = _jobRepository.Search(searchJob);
            return View(model);
        }
        [AllowAnonymous]
        [HttpGet]
        public ViewResult Applications()
        {
            var model = _applicationRepository.Search(searchApplication);
            return View(model);
        }
        
       [AllowAnonymous]
        public ViewResult About()
        {
            return View();
        }
        [AllowAnonymous]
        public ViewResult Contact()
        {
            return View();
        }

        public ViewResult Details(int? id)
        {

            Job job = _jobRepository.GetJob(id.Value);

            if (job == null)
            {
                Response.StatusCode = 404;
                return View("JobNotFound", id.Value);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Job = job,
                PageTitle = "Job Details"
            };

            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddJobViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "logos");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Job newJob = new Job
                {
                    Name = model.Name,
                    Position = model.Position,
                    Location = model.Location,
                    Type = model.Type,
                    Salary = model.Salary,
                    Department = model.Department,
                    Qualification = model.Qualification,
                    Skills = model.Skills,
                    Experience = model.Experience,
                    About = model.About,
                    pDate = model.pDate,
                    cDate = model.cDate,
                    ImagePath = uniqueFileName
                };

                _jobRepository.Add(newJob);
                return RedirectToAction("details", new { id = newJob.Id });
            }
            return View();
        }

        [HttpGet]
        public ViewResult Apply()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Apply(AddApplicationViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueResumeName = null;
                string uniqueIDName = null;
                string uniqueTranscriptName = null;
                string uniqueMatricName = null;
                string uniquerelDocsName = null;

                if (model.Resume != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "resumes");
                    uniqueResumeName = Guid.NewGuid().ToString() + "_" + model.Resume.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueResumeName);
                    model.Resume.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                if (model.ID != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "resumes");
                    uniqueIDName = Guid.NewGuid().ToString() + "_" + model.ID.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueIDName);
                    model.ID.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                if (model.Transcript != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "resumes");
                    uniqueTranscriptName = Guid.NewGuid().ToString() + "_" + model.Transcript.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueTranscriptName);
                    model.Transcript.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                if (model.matricCertificate != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "resumes");
                    uniqueMatricName = Guid.NewGuid().ToString() + "_" + model.matricCertificate.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueMatricName);
                    model.matricCertificate.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                if (model.relevantDocs != null && model.relevantDocs.Count > 0)
                {
                    foreach (IFormFile relDoc in model.relevantDocs)
                    {
                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "resumes");
                        uniquerelDocsName = Guid.NewGuid().ToString() + "_" + relDoc.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniquerelDocsName);
                        relDoc.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                }

                Application newApplication = new Application
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    Contact = model.Contact,
                    ResumePath = uniqueResumeName,
                    IdPath = uniqueIDName,
                    transcriptPath = uniqueTranscriptName,
                    matricCertificatePath = uniqueMatricName,
                    relevantDocsPath = uniquerelDocsName
                };

                _applicationRepository.Apply(newApplication);
                MailMessage mailMessage = new MailMessage("bennetprojects@gmail.com", model.Email);
                mailMessage.Subject = "Recruitment Application Confirmation";
                 string htmlString = "Dear " + model.Name + " " + model.Surname + @"<html>
                       <body>
                       <p> We would like to thank you for applying and confirm that we received your application. Our HR team will revert to you with feedback after viewing your application.</p>
                       <p>All the best,<br> Recruiting team</br></p>
                       </body>
                       </html>";
                mailMessage.Body = htmlString;
                mailMessage.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 25;
                smtpClient.EnableSsl = true;

                NetworkCredential networkCredential = new NetworkCredential("bennetprojects@gmail.com", "Ben@Pro01");
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                smtpClient.Send(mailMessage);
            }
            return View("ApplicationConfirmation");
        }

        private string ProcessUploadedFile(AddJobViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "logos");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Job job = _jobRepository.GetJob(id);
            EditJobViewModel jobEditViewModel = new EditJobViewModel
            {
                Id = job.Id,
                Name = job.Name,
                Position = job.Position,
                Location = job.Location,
                Type = job.Type,
                Salary = job.Salary,
                Department = job.Department,
                Qualification = job.Qualification,
                Skills = job.Skills,
                Experience = job.Experience,
                About = job.About,
                pDate = job.pDate,
                cDate = job.cDate,
                ExistingImagePath = job.ImagePath
            };
            return View(jobEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditJobViewModel model)
        {
            if (ModelState.IsValid)
            {
                Job job = _jobRepository.GetJob(model.Id);
                job.Name = model.Name;
                job.Position = model.Position;
                job.Location = model.Location;
                job.Type = model.Type;
                job.Salary = model.Salary;
                job.Department = model.Department;
                job.Qualification = model.Qualification;
                job.Skills = model.Skills;
                job.Experience = model.Experience;
                job.About = model.About;
                job.pDate = model.pDate;
                job.cDate = model.cDate;
                if (model.Photo != null)
                {
                    if (model.ExistingImagePath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath,
                            "images", model.ExistingImagePath);
                        System.IO.File.Delete(filePath);
                    }
                    job.ImagePath = ProcessUploadedFile(model);
                }
                Job updatedJob = _jobRepository.Update(job);

                return RedirectToAction("index");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var job = _jobRepository.GetJob(id);
            return View(job);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _jobRepository.Delete(id);
            return RedirectToAction("Index");
        }               
    }
}
