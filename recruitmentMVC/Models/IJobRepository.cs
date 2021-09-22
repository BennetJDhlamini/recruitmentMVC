using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recruitmentMVC.Models
{
    public interface IJobRepository
    {
        Job GetJob(int id);
        IEnumerable<Job> Search(string searchJob);
        IEnumerable<Job> GetAllJobs();
        Job Add(Job job);
        Job Update(Job jobChanges);
        Job Delete(int id);
    }
}
