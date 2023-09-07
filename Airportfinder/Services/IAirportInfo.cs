using Airportfinder.Models;

namespace Airportfinder.Services
{
    public interface IAirportInfo
    {
        List<AirportInfo> GetAirportById();
        AirportInfo GetAirportbyId(string Id);
        List<AirInfo> GetAirportsandDistance(string from, string to);
    }
}
