using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenPuertaEmployeesProject.Models
{
    public class EmployeetoSkill
    {
        public int ID { get; set; }
        public int EmployeeId { get; set; }
        public int SkillId { get; set; }
        public EmployeetoSkill() { }
    }
}