using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using Test2MVC.Models.Context;

namespace Test2MVC.Controllers
{
    public class Employee : Controller
    {

        AppDbContext ctx = new AppDbContext();

        public IActionResult Index()
        {
            //var emps = ctx.Employees.ToList();
            var empsDept = (from emp in ctx.Employees
                            join dept in ctx.Departments
                            on emp.DepartmentId equals dept.DepartmentID
                            select new Test2MVC.Models.Employee
                            {
                                EmployeeID = emp.EmployeeID,
                                EmployeeName = emp.EmployeeName,
                                EmployeeNumber = emp.EmployeeNumber,
                                DOB = emp.DOB,
                                HiringDate = emp.HiringDate,
                                GrossSalary = emp.GrossSalary,
                                NetSalary = emp.NetSalary,
                                DepartmentId = emp.DepartmentId,
                                DepartmentName= dept.DepartmentName,
                            }).ToList();
            return View(empsDept);
        }

        public IActionResult Create()
        {
            ViewBag.Department = this.ctx.Departments.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Test2MVC.Models.Employee employee)
        {
            ModelState.Remove("EmployeeID");
            ModelState.Remove("DepartmentName");
            ModelState.Remove("department");
            if (ModelState.IsValid)
            {
                ctx.Employees.Add(employee);
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Department = this.ctx.Departments.ToList();
            return View();
        }

        public IActionResult Edit(int ID)
        {
            var data = ctx.Employees.Where(e => e.EmployeeID== ID).FirstOrDefault();
            ViewBag.Department = this.ctx.Departments.ToList();
            return View("Create" , data);
        }

        [HttpPost]
        public IActionResult Edit(Test2MVC.Models.Employee employee)
        {
            ModelState.Remove("EmployeeID");
            ModelState.Remove("DepartmentName");
            ModelState.Remove("department");
            if (ModelState.IsValid)
            {
                ctx.Employees.Update(employee);
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Department = this.ctx.Departments.ToList();
            return View("Create", employee);
        }

        public IActionResult Delet(int ID)
        {
            var data = ctx.Employees.Where(e => e.EmployeeID == ID).FirstOrDefault();
            if (data != null)
            {
                ctx.Employees.Remove(data);
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
            
        }
    }
}
