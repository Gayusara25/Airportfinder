using Airportfinder.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var airportList = _airportInfoService.GetAllAirports().AsEnumerable();

            ViewBag.source = new SelectList(airportList, "AirportName", "AirportName");
            ViewBag.destination = new SelectList(airportList, "AirportName", "AirportName");

            return View();
        }

        [HttpPost]
        public IActionResult Cost(IFormCollection form)
        {
            string From = form["From1"].ToString();
            string To = form["To1"].ToString();

            Tuple<string, string> costDetails = _airportInfoService.GetCostDetails(From,To);

            TempData["dist"] = $"The distance between {From} and {To} is {costDetails.Item1} Kms, Cost incurred is   ";
            TempData["Cost"] = $"INR(₹):{costDetails.Item2}";

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

            if(!string.IsNullOrEmpty(State))
            {
                var statelist = _stateImgService.GetStateImgList();
                statelist = statelist.Where(x => x.State.ToLower().Contains(State.ToLower())).ToList();
                if (statelist.Count > 0)                
                    return View(statelist);                
            }           
            return View();           
        }

        public IActionResult AirportList(string id)
        {           
            return View (_airportInfoService.GetAirportsbyId(id));
        }

        public ActionResult Services()
        {
            return View();
        }
    }
}
