﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recruitmentMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Gen? Gender { get; set; }
        public string Location { get; set; }
    }
}
