using ErrorOr;
using Microsoft.EntityFrameworkCore;
using TowbyJobs.Data;
using TowbyJobs.Models;
using TowbyJobs.ServiceErrors;

namespace TowbyJobs.Services.Countries
{
    public class CountryService : ICountryService
    {
        private readonly AppDbContext _context;

        public CountryService(AppDbContext context)
        {
            _context = context;
        }

        public ErrorOr<Created> CreateCountry(Country country)
        {
            _context.Countries.Add(country);
            _context.SaveChanges();
            return Result.Created;
        }

        public ErrorOr<Deleted> DeletCountry(int id)
        {
            var country = _context.Countries.Find(id);
            if (country == null)
            {
                return Errors.Country.NotFound;
            }
            _context.Countries.Remove(country);
            _context.SaveChanges();
            return Result.Deleted;
        }

        public ErrorOr<Country> GetCountry(int id)
        {
            var country = _context.Countries.Find(id);
            return country == null ? Errors.Country.NotFound : country;
        }

        public ErrorOr<UpsertedCountryResult> UpsertCountry(Country country)
        {
            var comp = _context.Countries.Find(country.Country_Id);
            if (comp == null)
            {
                CreateCountry(country);
                return new UpsertedCountryResult(false);
            }
            else
            {
                _context.Countries.Update(country);
                _context.SaveChanges();
                return new UpsertedCountryResult(true);
            }

        }
    }
}
