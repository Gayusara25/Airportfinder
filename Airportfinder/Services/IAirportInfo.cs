using Airportfinder.Models;

namespace Airportfinder.Services
{
    public interface IAirportInfo
    {
        List<AirportInfo> GetAllAirports();
        List<AirportInfo> GetAirportsbyId(string Id);
        List<AirInfo> GetAirportsandDistance(string from, string to);
        Tuple<string,string> GetCostDetails(string from, string to);
    }
}
