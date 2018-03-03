using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

            string path = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "mysnapshot.json");
            if (System.IO.File.Exists(path))
            {
                string fileContent = System.IO.File.ReadAllText(path);
                getStarted = JsonConvert.DeserializeObject<Snapshot>(fileContent);
            }
                
            return Json(getStarted);
        }

        [HttpPost]
        public IActionResult PostSnapshot([FromBody] Snapshot snapshot)
        {
            var serialized = JsonConvert.SerializeObject(snapshot);
            
            string path = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "mysnapshot.json");
            System.IO.File.WriteAllText(path, serialized);

            return Json(new { });
        }

        public IActionResult Index()
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
