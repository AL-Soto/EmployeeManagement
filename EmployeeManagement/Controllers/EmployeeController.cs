using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        
        public ActionResult Index()
        {
            var employees = db.Employees.ToList();
            return View(employees);
            //return View(db.Employees.ToList());
        }
        public ActionResult AddEmployee()
        {
            return View();
        }
        public ActionResult UpdateEmployee(int id)
        {
            var employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }


        // GET: Employee/GetEmployees
        [HttpGet]
        public JsonResult GetEmployees()
        {
            var employees = db.Employees.ToList();
            return Json(employees, JsonRequestBehavior.AllowGet);
        }


        // POST: Employee/AddEmployee
        [HttpPost]

        public JsonResult AddEmployee([Bind(Include = "Id,Name,Position,Office,Salary")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return Json(new { success = true, employee = new { employee.Id, employee.Name, employee.Position, employee.Office, employee.Salary } });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, errors = errors });
        }



        // POST: Employee/UpdateEmployee
        [HttpPost]

        public JsonResult UpdateEmployee([Bind(Include = "Id,Name,Position,Office,Salary")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, message = "Employee updated successfully!" });
            }
            return Json(new { success = false, message = "Failed to update employee." });
        }
        // POST: Employee/DeleteEmployee
        [HttpPost]
        public JsonResult DeleteEmployee(int id)
        {
            try
            {
                var employee = db.Employees.Find(id);
                if (employee == null)
                {
                    return Json(new { success = false, message = "Employee not found." });
                }

                db.Employees.Remove(employee);
                db.SaveChanges();
                return Json(new { success = true, message = "Employee deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
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
