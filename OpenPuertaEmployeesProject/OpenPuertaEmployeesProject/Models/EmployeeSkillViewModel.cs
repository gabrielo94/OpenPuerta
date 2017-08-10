using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenPuertaEmployeesProject.Models
{
    public class EmployeeSkillViewModel
    {
        public Employee MyEmployee { get; set; }
        public List<Skillcheck> MySkills { get; set; }
    }
}