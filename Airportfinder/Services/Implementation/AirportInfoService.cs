using Airportfinder.Models;
using Airportfinder.RepositoryPattern;
using System.Data;
using System.Runtime;

namespace Airportfinder.Services.Implementation
{
    public class AirportInfoService : IAirportInfo
    {
        private readonly IRepository<AirportInfo> _airportRepository;
        private readonly ICityInfo _cityinfoService;

        public AirportInfoService(IRepository<AirportInfo> airportRepository, ICityInfo cityinfoService)
        {
            _airportRepository = airportRepository;
            _cityinfoService = cityinfoService;
        }

        public List<AirportInfo> GetAllAirports()
        {
            return _airportRepository.Get().ToList();
        }

        public List<AirportInfo> GetAirportsbyId(string Id)
        {
            return _airportRepository.Get().Where(x => x.State == Id).ToList();
        }


        public List<AirInfo> GetAirportsandDistance(string from, string to)
        {

            var cityList = _cityinfoService.GetCityList().AsEnumerable();

            CityInfo city1 = cityList.FirstOrDefault(m => m.CityName == from);
            CityInfo city2 = cityList.FirstOrDefault(m => m.CityName == to);

            var startlocation = new Location(city1.Latitude, city1.Longitude);
            var destinationlocation = new Location(city2.Latitude, city2.Longitude);

            var airportsInRange = new List<AirportInfo>(); /// airports between cities
            var airinrange = new List<AirInfo>();

            var airports = _airportRepository.Get();

            var maxDistance = HaversineFormula.HaversineDistance(startlocation, destinationlocation) + 50;
            foreach (var airport in airports)
            {
                var airportLocation = new Location(airport.Latitude, airport.Longitude);
                var distance = CalculateDistance(startlocation, destinationlocation, airportLocation);

                var dist = HaversineFormula.HaversineDistance(startlocation, airportLocation);

                if (distance <= maxDistance)
                {
                    AirInfo airinfodetails = new AirInfo();
                    airinfodetails.IataCode = airport.IataCode;
                    airinfodetails.City = airport.City;
                    airinfodetails.AirportName = airport.AirportName;
                    airinfodetails.Distance = dist;
                    airinrange.Add(airinfodetails);
                }
            }
            return airinrange = airinrange.OrderBy(a => a.Distance).ToList();
        }

        public Tuple<string, string> GetCostDetails(string from, string to)
        {

            AirportInfo airport1 = GetAirportInfoDetails(from);
            var startLocation = new Location(airport1.Latitude, airport1.Longitude);
            AirportInfo airport2 = GetAirportInfoDetails(to);
            var DestLocation = new Location(airport2.Latitude, airport2.Longitude);

            var maxDistance = HaversineFormula.HaversineDistance(startLocation, DestLocation);
            var rph = 14.54;
            double price = rph * maxDistance;
            price = Math.Round(price, 4);
            var dist = Math.Round(maxDistance, 4);
            return Tuple.Create(dist.ToString(), price.ToString());
        }


        private AirportInfo GetAirportInfoDetails(string airportName)
        {
            return _airportRepository.Get().AsEnumerable().FirstOrDefault(m => m.AirportName == airportName);
        }


        private double CalculateDistance(Location startLocation, Location destinationLocation, Location airportLocation)
        {
            var startToAirportDistance = HaversineFormula.HaversineDistance(startLocation, airportLocation);
            var airportToDestinationDistance = HaversineFormula.HaversineDistance(airportLocation, destinationLocation);
            var totalDistance = startToAirportDistance + airportToDestinationDistance;
            return totalDistance;
        }
    }
}