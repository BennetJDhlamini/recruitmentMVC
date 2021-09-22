using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recruitmentMVC.Models
{
    public interface IApplicationRepository
    {
        Application GetApplication(int id);
        IEnumerable<Application> GetAllApplications();
        Application Apply(Application application);
        IEnumerable<Application> Search(string searchApplication);
        Application Update(Application applicationChanges);
        Application Delete(int id);
    }
}
