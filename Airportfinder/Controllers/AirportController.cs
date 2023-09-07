using Airportfinder.Models;
using Airportfinder.Services;
using Airportfinder.Services.Implementation;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime;



namespace Airportfinder.Controllers
{
    public class AirportController : Controller
    {
        private readonly IAirportInfo _airportInfoService;

        private readonly ICityInfo _cityInfoService;
        private readonly IStateImg _stateImgService;

        public AirportController(IAirportInfo airportInfoService, ICityInfo cityInfoService, IStateImg stateImgService)
        {
            _airportInfoService = airportInfoService;
            _cityInfoService = cityInfoService;
            _stateImgService = stateImgService;
        }
        //public ActionResult FindAirport()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult FindAirport(string city1, string city2)
        //{
        //    if (string.IsNullOrWhiteSpace(city1) || string.IsNullOrWhiteSpace(city2))
        //    {
        //        return View();
        //    }
        //    var airports = _airportInfoService.FindAirport(city1, city2);
        //    ViewBag.City1 = city1;
        //    ViewBag.City2 = city2;
        //    return View(airports);
        //}

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            var cityList = _cityInfoService.GetCityList().AsEnumerable();

            ViewBag.source = new SelectList(cityList, "CITY", "CITY");

            ViewBag.destination = new SelectList(cityList, "CITY", "CITY");
            return View();
        }
        [HttpPost]
        public IActionResult Create(IFormCollection form)
        {
            string From = form["source"].ToString();
            string To = form["destination"].ToString();
            if (From == To)
            {
                TempData["Error"] = "Source and destination cannot be same";
                return RedirectToAction("Create");
            }
            else
                return View("AirportDisplay", _airportInfoService.GetAirportsandDistance(From, To));
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Cost()
        {
            var airportList = _airportInfoService.GetAirportById().AsEnumerable();

            ViewBag.source = new SelectList(airportList, "AirportName", "AirportName");

            ViewBag.destination = new SelectList(airportList, "AirportName", "AirportName");
            return View();
          
        }

        [HttpPost]

        public IActionResult Cost(IFormCollection form)
        {
            var airportList = _airportInfoService.GetAirportById().AsEnumerable();
            string From= form["From1"].ToString();
            string To = form["To1"].ToString();
            
            AirportInfo airport1= airportList.FirstOrDefault(m=>m.AirportName == From);
            var startLocation = new Location(airport1.Latitude,airport1.Longitude);
            AirportInfo airport2 = airportList.FirstOrDefault(m => m.AirportName == To);
            var DestLocation = new Location(airport2.Latitude, airport2.Longitude);

            var maxDistance = HaversineFormula.HaversineDistance(startLocation, DestLocation);
            var rph = 14.54;
            double price = rph * maxDistance;
            price = Math.Round(price, 4);
            var dist = Math.Round(maxDistance, 4);

            TempData["dist"] = $"The distance between {From} and {To} is {dist} Kms, Cost incurred is   ";
            TempData["Cost"] = $"INR(₹):{price}";

            return View();
        }
        public IActionResult StateWiseAirports()
        {
            var state = _stateImgService.GetStateImgList();
            return View(state);
        }

        [HttpPost]
        public IActionResult StateWiseAirports(string State)
        {
            var statelist = _stateImgService.GetStateImgList();
            var StateList = statelist.Where(x => x.State.ToLower().Contains(State.ToLower())).ToList();
            if (StateList.Count > 0)
            {
                return View(StateList);
            }
            return View();
           
        }

        public IActionResult AirportList(string id)
        {
            var airports = _airportInfoService.GetAirportById();
            List<AirportInfo> list = airports.FindAll(m => m.State == id);
            return View (list);





        }
        public ActionResult Services()
        {
            return View();
        }
    }
}
