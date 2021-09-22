using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recruitmentMVC.ViewModels
{
    public class EditJobViewModel : AddJobViewModel
    {
        public int Id { get; set; }
        public string ExistingImagePath { get; set; }
    }
}
