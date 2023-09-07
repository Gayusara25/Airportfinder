using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Airportfinder.Models
{
   
    public class StateImg
    {
        [Key]
        public string State { get; set; }

        public string Photo { get; set; }
    }
}
