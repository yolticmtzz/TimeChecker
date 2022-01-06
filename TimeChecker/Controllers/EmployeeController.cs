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
        // Variable für Datenbankinhalt
        private readonly ApplicationDbContext _context;

        // Dependency injection Übergabe des Datenbankinhalts
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Datenbankinhalt Employees in Variable employees speichern
            var employees = _context.Employees.ToList();

            // Variable employees in ViewBag übergeben
            ViewBag.Employees = employees;

            // ViewBag wird übergeben
            return View();
        }

        // Bestehender Employee aus Datenbank bearbeiten
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

        // Employee hinzufügen oder updaten wenn ID nicht 0 
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

            // Weiterleitung zur Indexseite
            return RedirectToAction("Index");     
        }

        // Employee löschen
        public IActionResult DeleteEmployee(int id)
        {
            var employeeInDb = _context.Employees.Find(id);

            if (employeeInDb == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employeeInDb);
            _context.SaveChanges();

            // Weiterleitung zur Indexseite
            return RedirectToAction("Index");

        }



    }
}
