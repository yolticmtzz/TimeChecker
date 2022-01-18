using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeChecker.DAL.Data;
using TimeChecker.DAL.Models;
using System.IO;

/**
 * Die Klasse TimeentryController ist die Schnittstelle zwischen 
 * Datenbank und den HTMl Seiten (View's) im Ordner "Views" -> "Timeentry" angelegt.
 * Den Datenbank Inhalt wird über den Konstruktor übergeben.
 * 
 * Methoden: 
 * 
 * Index() - Die Hauptseite von Timeentry View, hier wird der Datenbank Inhalt 
 * auf der Webseite übergeben und angezeigt.
 * 
 * CreateEdit() - Bestehender Timeentry bearbeiten
 * 
 * CreateEditEmployee() - Bestehender Timeentry bearbeiten updaten oder neuer hinzufügen
 * 
 * DeleteEmployee() - Bestehender Timeentry bearbeiten
 * 
 * Export() - Exportiert den Inhalt der Datenbank von Timeentry von der Webseite in eine Text Datei.
 * 
 * SaveWithStream() - Methode zum Speichern des Datenbank Inhalts von Timeentry
 * 
 * 
 * @Author Jose Panov
 * @Version 2022.01.01
 */

namespace TimeChecker.Controllers
{
    [Authorize]
    public class TimeentryController : Controller
    {
        // Variable für Datenbankinhalt
        private readonly ApplicationDbContext _context;

        const string pathtxt = @"C:\temp\Timeentry_Data.txt";

        // Dependency injection Übergabe des Datenbankinhalts
        public TimeentryController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Datenbank Inhalt Timeentry in Variable timeentry speichern
        /// Variable timeentry in ViewBag übergeben
        /// </summary>
        /// 
        /// <returns>
        /// ViewBag wird übergeben
        /// </returns>
        public IActionResult Index()
        {
            var timeentry = _context.Timeentry.ToList();
        
             ViewBag.Timeentry = timeentry;
            
             return View();            
        }

        /// <summary>
        /// Bestehender Timeentry aus Datenbank bearbeiten
        /// </summary>
        /// 
        /// <param name="id"> 
        /// Timeentry mit entsprechender id suchen
        /// </param>
        /// 
        /// <returns>
        /// View CreateEditTimeentry
        /// </returns>
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

        /// <summary>
        /// Timeentry hinzufügen oder updaten wenn ID nicht 0 
        /// </summary>
        /// 
        /// <param name="timeentry"> 
        /// Model Timeentry übergeben 
        /// </param>
        /// 
        /// <returns>
        /// View Index Seite
        /// </returns>
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

        /// <summary>
        /// Timeentry löschen
        /// </summary>
        /// 
        /// <param name="id"> 
        /// Timeentry mit entsprechender id suchen
        /// </param>
        /// 
        /// <returns>
        /// View Index Seite
        /// </returns>
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

        /// <summary>
        /// Timeentry Daten in Textdatei exportieren
        /// </summary>
        /// 
        /// <returns>
        /// View Index Seite
        /// </returns>
        public IActionResult Export()
        {
            var timeentryinDB = _context.Timeentry.ToList();

            SaveWithStream(pathtxt, timeentryinDB);

            return RedirectToAction("Index");
        }
 
        /// <summary>
        /// Textdatei via stream speichern
        /// </summary>
        /// 
        /// <param name="path">
        /// Zielpfad des textfiles 
        /// </param>
        /// 
        /// <param name="timeentryinDB">
        /// Liste von Timeentry übergeben
        /// </param>
        private static void SaveWithStream(string path, List<Timeentry> timeentryinDB)
        {
            var convertString = "";
            var commentString = "";

            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (TextWriter writer = new StreamWriter(fs))
            {
                // Header für Excel
                writer.Write("ID;");
                writer.Write("Type;");
                writer.Write("DateTime;");
                writer.Write("Comment;");
                writer.Write("User");
                writer.WriteLine();

                foreach (var timeentry in timeentryinDB)
                {
                        switch (timeentry.Type)
                        {
                            case 1:
                            convertString = timeentry.Type.ToString("Check In");
                            break;
                            
                            case 2:
                            convertString = timeentry.Type.ToString("Check Out");
                            break;

                            case 3:
                            convertString = timeentry.Type.ToString("Start Break");
                            break;

                            case 4:
                            convertString = timeentry.Type.ToString("Stop Break");
                            break;

                            default: 
                            break;
                        }

                        switch (timeentry.Type)
                        {
                            case 1:
                            commentString = "Leer";
                            break;

                            case 2:
                            commentString = timeentry.Comment;
                            break;

                            case 3:
                            commentString = "Leer";
                            break;

                            case 4:
                            commentString = "Leer";
                            break;

                            default:
                            break;
                        }

                    // Struktur passend für Excel
                    writer.Write(timeentry.ID);
                    writer.Write(";");
                    writer.Write(convertString);
                    writer.Write(";");
                    writer.Write(timeentry.DateTime);
                    writer.Write(";");
                    writer.Write(commentString);
                    writer.Write(";");
                    writer.Write(timeentry.User);
                    writer.WriteLine();
                }                            
            }
        }
    }
}
