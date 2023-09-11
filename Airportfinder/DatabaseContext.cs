using Airportfinder.Models;
using Microsoft.EntityFrameworkCore;

namespace Airportfinder
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opts) : base(opts) { }
        public DbSet<AirportInfo> Airportinfo { get; set; }
        public DbSet<CityInfo> Cityinfo { get; set; }
        public DbSet<StateImg> StateImg { get; set; }
    }
}
