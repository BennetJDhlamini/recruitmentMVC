using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recruitmentMVC.Models
{
    public class SQLJobRepository : IJobRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<SQLJobRepository> logger;

        public SQLJobRepository(ApplicationDbContext context, ILogger<SQLJobRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Job Add(Job job)
        {
            context.Jobs.Add(job);
            context.SaveChanges();
            return job;
        }

        public Job Delete(int Id)
        {
            Job job = context.Jobs.Find(Id);
            if (job != null)
            {
                context.Jobs.Remove(job);
                context.SaveChanges();
            }
            return job;
        }

        public IEnumerable<Job> GetAllJobs()
        {
            return context.Jobs;
        }

        public Job GetJob(int Id)
        {
            return context.Jobs.Find(Id);
        }

        public IEnumerable<Job> Search(string searchJob = null)
        {
            if (string.IsNullOrEmpty(searchJob))
            {
                return context.Jobs;
            }

            return context.Jobs.Where(e => e.Name.Contains(searchJob) || e.Position.Contains(searchJob) || e.Location.Contains(searchJob)).ToList();
        }

        public Job Update(Job jobChanges)
        {
            var job = context.Jobs.Attach(jobChanges);
            job.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return jobChanges;
        }
    }
}
