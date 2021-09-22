using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recruitmentMVC.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>().HasData(
                new Job
                {
                    Id = 1,
                    Name = "FNB",
                    Position = "Software Development",
                    Location = "Johannesburg",
                    Type = Typ.Permanent,
                    Salary = 27000,
                    Department = Department.IT,
                    Qualification = "Degree",
                    Skills = "C#,.NetCore MVC",
                    Experience = "Entry-Level/Graduate",
                    About = "Graduate program",
                    pDate = "22.04.2021",
                    cDate = "30.08.21"
                }
                
            ); ;
        }
    }
}
