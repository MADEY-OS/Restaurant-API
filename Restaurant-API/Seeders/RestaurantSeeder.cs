using Restaurant_API.Entities;

namespace Restaurant_API.Seeders;

public class RestaurantSeeder
{
    private readonly RestaurantDbContext _dbContext;

    public RestaurantSeeder(RestaurantDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        if (_dbContext.Database.CanConnect())
            if (!_dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                _dbContext.Restaurants.AddRange(restaurants);
                _dbContext.SaveChanges();
            }
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        var restaurants = new List<Restaurant>
        {
            new()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description = "KFC is an American fast food restaurant.",
                ContactEmail = "contact@kfc.com",
                ContactNumber = "111 222 333",
                HasDelivery = true,
                Dishes = new List<Dish>
                {
                    new()
                    {
                        Name = "Strips Deluxe Box",
                        Price = 29.22M,
                        Description = "Big box with strips and wings"
                    },
                    new()
                    {
                        Name = "Longer",
                        Price = 15.66M,
                        Description = "Long Sandwitch with chicken strips."
                    }
                },
                Address = new Address
                {
                    City = "Oświęcim",
                    Street = "Powstańców Oświęcimskich 23",
                    PostalCode = "32-600"
                }
            },
            new()
            {
                Name = "McDonalds",
                Category = "Fast Food",
                Description = "McDonalds is an American fast food restaurant.",
                ContactEmail = "contact@mcdonalds.com",
                ContactNumber = "444 555 666",
                HasDelivery = true,
                Dishes = new List<Dish>
                {
                    new()
                    {
                        Name = "BigMac",
                        Price = 9.22M,
                        Description = "Classic burgier."
                    },
                    new()
                    {
                        Name = "ChickenBox",
                        Price = 33.66M,
                        Description = "A box full of strips and nuggets"
                    }
                },
                Address = new Address
                {
                    City = "Oświęcim",
                    Street = "Powstańców Oświęcimskich 24B",
                    PostalCode = "32-600"
                }
            }
        };
        return restaurants;
    }
}