using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recruitmentMVC.Models
{
    public class SQLApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<SQLApplicationRepository> logger;
        public SQLApplicationRepository(ApplicationDbContext context, ILogger<SQLApplicationRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Application Apply(Application application)
        {
            context.Applications.Add(application);
            context.SaveChanges();
            return application;
        }

        public Application Delete(int id)
        {
            Application application = context.Applications.Find(id);
            if (application != null)
            {
                context.Applications.Remove(application);
                context.SaveChanges();
            }
            return application;
        }

        public IEnumerable<Application> GetAllApplications()
        {
            return context.Applications;
        }

        public Application GetApplication(int id)
        {
            return context.Applications.Find(id);
        }

        public IEnumerable<Application> Search(string searchApplication)
        {
            if (string.IsNullOrEmpty(searchApplication))
            {
                return context.Applications;
            }

            return context.Applications.Where(e => e.Name.Contains(searchApplication) || e.Surname.Contains(searchApplication) || e.Email.Contains(searchApplication) || e.Contact.Contains(searchApplication)).ToList();
        }

        public Application Update(Application applicationChanges)
        {
            var application = context.Applications.Attach(applicationChanges);
            application.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return applicationChanges;
        }
    }
}
