using System.ComponentModel.DataAnnotations;

namespace Airportfinder.Models
{
    public class AirportInfo
    {
        [Key]
        public string IataCode { get; set; }
        [Required]
        public string AirportName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    
    }
}
