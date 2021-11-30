using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeChecker.DAL.Data;
using TimeChecker.DAL.Models;

namespace TimeChecker.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();

            ViewBag.Employees = employees;


            return View();
        }


        public IActionResult CreateEdit(int id)
        {
            if (id == 0)
            {
                return View("CreateEditEmployee");
            }
            
            var employeeInDb = _context.Employees.Find(id);

            if (employeeInDb == null)
            {
                return NotFound();
            }

            return View("CreateEditEmployee",employeeInDb);

        }


        [HttpPost]
        public IActionResult CreateEditEmployee(Employee employee)
        {
            if (employee.Id == 0)
            {
                _context.Employees.Add(employee);
            }
            else
            {
                _context.Employees.Update(employee);         
            }

            _context.SaveChanges();

            return RedirectToAction("Index");     
        }

        
        public IActionResult DeleteEmployee(int id)
        {
            var employeeInDb = _context.Employees.Find(id);

            if (employeeInDb == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employeeInDb);
            _context.SaveChanges();


            return RedirectToAction("Index");

        }



    }
}
