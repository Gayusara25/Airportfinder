using Airportfinder.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Airportfinder.Services
{
    public interface ICityInfo
    {
        List<CityInfo> GetCityList();
    }
}
