using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenPuertaEmployeesProject.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public DateTime Birthdate { get; set; }
        public List<Skill> Skills { get; set; }
        public double Salary { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Employee() { }

    }
}