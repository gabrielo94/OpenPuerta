using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenPuertaEmployeesProject.Models
{
    public class Skill
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }

        public Skill() { }
    }
}