using ErrorOr;
using TowbyJobs.Models;

namespace TowbyJobs.Services.Cities
{
    public interface ICityService
    {
        ErrorOr<Created> CreateCity(City city);
        ErrorOr<City> GetCity(int id);
        ErrorOr<List<City>> GetCities(int number);
        ErrorOr<UpsertedCityResult> UpsertCity(City city);
        ErrorOr<Deleted> DeletCity(int id);
    }
}
