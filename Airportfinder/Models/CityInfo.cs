using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Airportfinder.Models
{
    
    public class CityInfo
    {
        [Key]
        public string CityName { get; set; }
       
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
