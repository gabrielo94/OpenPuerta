using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OpenPuertaEmployeesProject.Models;
using Newtonsoft.Json;

namespace OpenPuertaEmployeesProject.Controllers
{
    public class EmployeesController : Controller
    {
        private OpenPuertaEmployeesProjectContext db = new OpenPuertaEmployeesProjectContext();

        /// <summary>
        /// Api for employees
        /// </summary>
        /// <returns>
        /// returns all employees in json format</returns>
        public ActionResult Api()
        {
            
            string result = JsonConvert.SerializeObject(db.Employees.ToList());

            return Content(result);
        }

        // GET: Employees
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            var skillsIdQuery = from employeeskill in db.EmployeetoSkill join skill in db.Skills 
                                on employeeskill.SkillId equals skill.ID
                                where employeeskill.EmployeeId == employee.ID
                                select skill;
            employee.Skills = skillsIdQuery.ToList();
            List<Skillcheck> skills = new List<Skillcheck>();
            
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Birthdate,Salary")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }


        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);

            var skillscheksQuery = (from skill in db.Skills
                                    select new Skillcheck()
                                    {
                                        SkillName = skill.Name,
                                        SkillID = skill.ID,
                                        Checked = ((from employeeskill in db.EmployeetoSkill
                                                    where employeeskill.SkillId == skill.ID & employeeskill.EmployeeId == employee.ID
                                                    select employeeskill).Count() > 0)
                                    }
            );
            List<Skillcheck>  skillschecked = skillscheksQuery.ToList();
            for (int i = 0; i < skillschecked.Count; i++)
            {

            }

            EmployeeSkillViewModel Employeesandskills = new EmployeeSkillViewModel()
            {
                MyEmployee = employee,
                MySkills = skillschecked
            };
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(Employeesandskills);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeSkillViewModel employee)
        {
            if (ModelState.IsValid)
            {
                var MyEmployee = db.Employees.Find(employee.MyEmployee.ID);

                MyEmployee.Name = employee.MyEmployee.Name;
                MyEmployee.Salary = employee.MyEmployee.Salary;
                MyEmployee.Birthdate = employee.MyEmployee.Birthdate;

                foreach (var item in db.EmployeetoSkill)
                {
                    if(item.EmployeeId == employee.MyEmployee.ID)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
                foreach (var item in employee.MySkills)
                {
                    if (item.Checked)
                    {
                        db.EmployeetoSkill.Add(new EmployeetoSkill() { EmployeeId = employee.MyEmployee.ID, SkillId = item.SkillID });
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            foreach (var item in db.EmployeetoSkill)
            {
                if (item.EmployeeId == employee.ID)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }
            }
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
