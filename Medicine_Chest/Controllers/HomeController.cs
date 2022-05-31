using Medicine_Chest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Medicine_Chest.Controllers
{
  
    public class HomeController : Controller
    {  
  
        private readonly ILogger<HomeController> _logger;
   
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public ıactionresult ındex()
        //{
        //    return view();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize]
        public ActionResult Index()
		{
			List<DataPoint> dataPoints = new List<DataPoint>();

			dataPoints.Add(new DataPoint("Ocak", 2500));
			dataPoints.Add(new DataPoint("Şubat", 2790));
			dataPoints.Add(new DataPoint("Mart", 1578));
			dataPoints.Add(new DataPoint("Nisan", 3890));
			dataPoints.Add(new DataPoint("Mayıs", 2340));
			dataPoints.Add(new DataPoint("Haziran", 3125));
			dataPoints.Add(new DataPoint("Temmuz", 4280));
			dataPoints.Add(new DataPoint("Ağustos", 3650));
			dataPoints.Add(new DataPoint("Eylül", 3370));
			dataPoints.Add(new DataPoint("Ekim", 2160));
			dataPoints.Add(new DataPoint("Kasım", 2800));
			dataPoints.Add(new DataPoint("Aralık", 3380));
			

			ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

			return View();
		}
	}
}
