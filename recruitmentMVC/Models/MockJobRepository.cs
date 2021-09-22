using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recruitmentMVC.Models
{
    public class MockJobRepository : IJobRepository
    {
        private List<Job> _jobList;

        public IWebHostEnvironment hostingEnvironment;

        public MockJobRepository(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        public Job GetJob(int id)
        {
            return this._jobList.FirstOrDefault(e => e.Id == id);
        }
        public IEnumerable<Job> GetAllJobs()
        {
            return _jobList;
        }

        public Job Add(Job job)
        {
            job.Id = _jobList.Max(e => e.Id) + 1;
            _jobList.Add(job);
            return job;
        }

        public Job Update(Job jobChanges)
        {
            Job job = _jobList.FirstOrDefault(e => e.Id == jobChanges.Id);
            if (job != null)
            {
                job.Name = jobChanges.Name;
                job.Position = jobChanges.Position;
                job.Location = jobChanges.Location;
                job.Type = jobChanges.Type;
                job.Salary = jobChanges.Salary;
                job.Department = jobChanges.Department;
                job.Qualification = jobChanges.Qualification;
                job.Skills = jobChanges.Skills;
                job.Experience = jobChanges.Experience;
                job.About = jobChanges.About;
                job.pDate = jobChanges.pDate;
                job.cDate = jobChanges.cDate;
                job.ImagePath = jobChanges.ImagePath;
            }
            return job;
        }

        public Job Delete(int id)
        {
            Job job = _jobList.FirstOrDefault(e => e.Id == id);
            if (job != null)
            {
                _jobList.Remove(job);
            }
            return job;
        }

        public IEnumerable<Job> Search(string searchJob = null)
        {
            if (string.IsNullOrEmpty(searchJob))
            {
                return _jobList;
            }

            return _jobList.Where(e => e.Name.Contains(searchJob) || e.Position.Contains(searchJob)).ToList();
        }
    }
}