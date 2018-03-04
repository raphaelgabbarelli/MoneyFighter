using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using moneelife.web.Logic;
using Microsoft.AspNetCore.Mvc;
using moneelife.web.Models;
using Newtonsoft.Json;

namespace moneelife.web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Snapshot()
        {
            Snapshot getStarted = new Snapshot();

            decimal totalCash = 0;
            decimal totalGoals = 0;
            decimal totalContribution = 0;
            decimal? available = 0;

            string path = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "mysnapshot.json");
            if (System.IO.File.Exists(path))
            {
                string fileContent = System.IO.File.ReadAllText(path);
                getStarted = JsonConvert.DeserializeObject<Snapshot>(fileContent);

                if (getStarted?.Accounts?.Count > 0)
                {
                    totalCash = getStarted.Accounts.Sum(a => a.Balance);
                }

                if (getStarted?.Goals?.Count > 0)
                {
                    totalGoals = getStarted.Goals.Sum(g => g.Amount);
                    totalContribution = getStarted.Goals.Sum(g => g.CurrentValue);
                }
            }

            available = totalCash - totalContribution - (getStarted?.Emergency?.ActualAmount);
                
            return Json(new { snapshot = getStarted, totalCash = totalCash, totalGoals = totalGoals, totalContribution = totalContribution, available = available });
        }

        [HttpPost]
        public IActionResult PostSnapshot([FromBody] Snapshot snapshot)
        {
            // set all goal start date to march 1st 2018
            snapshot.Goals.ForEach(g => g.StartDate = new DateTime(2018, 3, 1));

            Algorithm a = new Algorithm();
            a.CrunchNumbers(ref snapshot);
            var serialized = JsonConvert.SerializeObject(snapshot);
            
            string path = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "mysnapshot.json");
            System.IO.File.WriteAllText(path, serialized);

            return Json(new { snapshot = snapshot });
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Grid()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
