using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeChecker.DAL.Data;
using TimeChecker.DAL.Models;
using TimeChecker.Models;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;

namespace TimeChecker.Controllers
{
    [Authorize]
    public class TimeentryController : Controller
    {
        // Variable für Datenbankinhalt
        private readonly ApplicationDbContext _context;

        const string path = @"C:\Users\jopa\Desktop\Test Filestream\SerializeData.txt";





        // Dependency injection Übergabe des Datenbankinhalts
        public TimeentryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Export()
        {
            //SerializeData SD = new SerializeData("Test");
            //BinaryFormatter Formatter = new BinaryFormatter();
            //FileStream stream = new FileStream(@"C:\Users\jopa\Desktop\Test Filestream\SerializeData.txt", FileMode.Create, FileAccess.Write);
            var textToSave = "Hallo";

            SaveWithStream(path, textToSave);
            //ReadWithStream(path);

            return View("Index");
        }

        private static void SaveWithStream(string path, string textToSave)
        {
            using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                var encodedText = Encoding.ASCII.GetBytes(textToSave);
                fs.Write(encodedText, 0, encodedText.Length);
            }
        }

        public IActionResult Index()
        {

            // Datenbankinhalt Timeentry in Variable employees speichern
            var timeentry = _context.Timeentry.ToList();
        
            // Variable timeentry in ViewBag übergeben
             ViewBag.Timeentry = timeentry;
            
             // ViewBag wird übergeben
             return View();            
        }

        // Bestehender Timeentry aus Datenbank bearbeiten
        public IActionResult CreateEdit(int id)
        {
            if (id == 0)
            {
                return View("CreateEditTimeentry");
            }

            var timeentryInDb = _context.Timeentry.Find(id);

            if (timeentryInDb == null)
            {
                return NotFound();
            }

            return View("CreateEditTimeentry", timeentryInDb);

        }

        // Timeentry hinzufügen oder updaten wenn ID nicht 0 
        [HttpPost]
        public IActionResult CreateEditTimeentry(Timeentry timeentry)
        {
            if (timeentry.ID == 0)
            {
                _context.Timeentry.Add(timeentry);
            }
            else
            {
                _context.Timeentry.Update(timeentry);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // Timeentry löschen
        public IActionResult DeleteTimeentry(int id)
        {
            var timeentryInDb = _context.Timeentry.Find(id);

            if (timeentryInDb == null)
            {
                return NotFound();
                
            }

            _context.Timeentry.Remove(timeentryInDb);
            _context.SaveChanges();


            return RedirectToAction("Index");

        }
    }
}
