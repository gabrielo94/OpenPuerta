using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OpenPuertaEmployeesProject.Models
{
    public class OpenPuertaEmployeesProjectContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public OpenPuertaEmployeesProjectContext() : base("name=OpenPuertaEmployeesProjectContext")
        {
        }
        
        public System.Data.Entity.DbSet<OpenPuertaEmployeesProject.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<OpenPuertaEmployeesProject.Models.EmployeetoSkill> EmployeetoSkill { get; set; }

        public System.Data.Entity.DbSet<OpenPuertaEmployeesProject.Models.Skill> Skills { get; set; }
    }
}
