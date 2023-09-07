using Airportfinder.Models;
using Airportfinder.RepositoryPattern;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Airportfinder.Services.Implementation
{
    public class CityInfoService : ICityInfo
    {
        private readonly IRepository<CityInfo> _cityRepository;
        public CityInfoService(IRepository<CityInfo> cityRepository)
        {

            _cityRepository = cityRepository;
        }

        public List<CityInfo> GetCityList()
        {
          return  _cityRepository.Get().ToList();
        }

    }
}
