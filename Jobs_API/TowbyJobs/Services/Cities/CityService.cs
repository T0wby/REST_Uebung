using ErrorOr;
using TowbyJobs.Data;
using TowbyJobs.Models;
using TowbyJobs.ServiceErrors;

namespace TowbyJobs.Services.Cities
{
    public class CityService : ICityService
    {
        private readonly AppDbContext _context;

        public CityService(AppDbContext context)
        {
            _context = context;
        }

        public ErrorOr<Created> CreateCity(Models.City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();
            return Result.Created;
        }

        public ErrorOr<Deleted> DeletCity(int id)
        {
            var city = _context.Cities.Find(id);
            if (city == null)
            {
                return Errors.City.NotFound;
            }
            _context.Cities.Remove(city);
            _context.SaveChanges();
            return Result.Deleted;
        }

        public ErrorOr<List<City>> GetCities(int number)
        {
            if (number <= 0)
            {
                return Errors.Job.OutOfScope;
            }

            if (number > _context.Cities.Count()) number = _context.Cities.Count();

            var cities = _context.Cities.Take(number).ToList();
            return cities;
        }

        public ErrorOr<Models.City> GetCity(int id)
        {
            var city = _context.Cities.Find(id);
            return city == null ? Errors.City.NotFound : city;
        }

        public ErrorOr<UpsertedCityResult> UpsertCity(Models.City city)
        {
            var comp = _context.Cities.Find(city.City_Id);
            if (comp == null) 
            {
                CreateCity(city); 
                return new UpsertedCityResult(false);
            }
            else
            {
                _context.Cities.Update(city);
                _context.SaveChanges();
                return new UpsertedCityResult(true);
            }

        }
    }
}
