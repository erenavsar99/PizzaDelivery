using InfosetPizza.Data;
using InfosetPizza.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace InfosetPizza.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class RestaurantBranchesController : Controller
    {
        private readonly InfosetPizzaDbContext _infosetPizzaDbContext;

        public RestaurantBranchesController(InfosetPizzaDbContext infosetPizzaDbContext)
        {
            _infosetPizzaDbContext = infosetPizzaDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetRestaurantsNearBy(double latitude, double longitude)
        {
            var R = 6371; // dünya yarıçapı (km)

            var restaurants = await _infosetPizzaDbContext.RestaurantBranches.ToListAsync();

            //distance'ı hesaplarken chatGPT'den yardım aldım. 

            var nearbyRestaurants = restaurants
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    Distance = 2 * R * Math.Asin(Math.Sqrt(
                Math.Pow(Math.Sin((latitude - x.Latitude) * Math.PI / 360), 2) +
                Math.Cos(latitude * Math.PI / 180) * Math.Cos(x.Latitude * Math.PI / 180) *
                Math.Pow(Math.Sin((longitude - x.Longitude) * Math.PI / 360), 2)
                    ))
                })
                .Where(x => x.Distance <= 10)
                .OrderBy(x => x.Distance)
                .Take(5)
                .ToList();

            return Ok(nearbyRestaurants);
        }
    }
}
