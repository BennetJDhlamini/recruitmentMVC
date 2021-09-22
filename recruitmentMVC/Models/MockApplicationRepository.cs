using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recruitmentMVC.Models
{
    public class MockApplicationRepository : IApplicationRepository
    {
        private List<Application> _applicationList;

        public Application Apply(Application application)
        {
            application.Id = _applicationList.Max(e => e.Id) + 1;
            _applicationList.Add(application);
            return application;
        }

        public Application Delete(int id)
        {
            Application application = _applicationList.FirstOrDefault(e => e.Id == id);
            if (application != null)
            {
                _applicationList.Remove(application);
            }
            return application;
        }

        public IEnumerable<Application> GetAllApplications()
        {
            return _applicationList;
        }

        public Application GetApplication(int id)
        {
            return this._applicationList.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Application> Search(string searchApplication)
        {
            if (string.IsNullOrEmpty(searchApplication))
            {
                return _applicationList;
            }

            return _applicationList.Where(e => e.Name.Contains(searchApplication) || e.Email.Contains(searchApplication)).ToList();
        }

        public Application Update(Application applicationChanges)
        {
            Application application = _applicationList.FirstOrDefault(e => e.Id == applicationChanges.Id);
            if (application != null)
            {
                application.Name = applicationChanges.Name;
                application.Surname = applicationChanges.Surname;
                application.Email = applicationChanges.Email;
                application.ResumePath = applicationChanges.ResumePath;
                application.IdPath = applicationChanges.IdPath;
                application.transcriptPath = applicationChanges.transcriptPath;
                application.matricCertificatePath = applicationChanges.matricCertificatePath;
                application.relevantDocsPath = applicationChanges.relevantDocsPath;
            }
            return application;
        }
    }
}
