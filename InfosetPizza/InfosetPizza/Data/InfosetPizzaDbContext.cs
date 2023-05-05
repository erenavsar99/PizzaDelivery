using Microsoft.EntityFrameworkCore;
using InfosetPizza.Models;

namespace InfosetPizza.Data
{
    public class InfosetPizzaDbContext : DbContext
    {
        public InfosetPizzaDbContext(DbContextOptions options) : base(options) 
        {

        }

        public DbSet<RestaurantBranches> RestaurantBranches { get; set; }
    }
}
