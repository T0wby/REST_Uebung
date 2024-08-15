using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using TowbyJobs.Contracts.City;
using TowbyJobs.Contracts.Company;
using TowbyJobs.Models;
using TowbyJobs.Services.Cities;

namespace TowbyJobs.Controllers
{
    public class CityController : ApiController
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpPost()]
        public IActionResult CreateCity(CreateCityRequest request)
        {
            var city = MapCity(request, 0);

            if (city.IsError)
            {
                return Problem(city.Errors);
            }

            var createCityResult = _cityService.CreateCity(city.Value);

            return createCityResult.Match(
               created => CreatedAtGetCity(city.Value),
               errors => Problem(errors));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetCity(int id)
        {
            ErrorOr<City> getCityResult = _cityService.GetCity(id);

            return getCityResult.Match(
                city => Ok(MapCityResponse(city)),
                errors => Problem(errors));
        }

        [HttpGet("getCities/{count:int}")]
        public IActionResult GetCityList(int count)
        {
            var getCityResult = _cityService.GetCities(count);

            return getCityResult.Match(
                cities => Ok(MapCityListResponse(cities)),
                errors => Problem(errors));
        }

        [HttpPut("{id:int}")]
        public IActionResult UpsertCity(int id, UpsertCityRequest request)
        {
            var city = MapCity(request, id);

            if (city.IsError)
            {
                return Problem(city.Errors);
            }

            var updateCityResult = _cityService.UpsertCity(city.Value);


            return updateCityResult.Match(
                updated => updated.IsNewlyCreated ? CreatedAtGetCity(city.Value) : NoContent(),
                errors => Problem(errors)
                );
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteCity(int id)
        {
            var deleteCityResult = _cityService.DeletCity(id);


            return deleteCityResult.Match(
                deleted => NoContent(),
                errors => Problem(errors)
                );
        }

        #region Mappings
        private static CityResponse MapCityResponse(City city)
        {
            return new CityResponse(
                            city.City_Id,
                            city.Name,
                            city.AreaCode,
                            city.LastTimeUpdated
                            );
        }
        private static List<CityResponse> MapCityListResponse(List<City> cities)
        {
            List<CityResponse> responses = new List<CityResponse>();

            foreach (var city in cities)
            {
                responses.Add(
                new CityResponse(
                        city.City_Id,
                        city.Name,
                        city.AreaCode,
                        city.LastTimeUpdated
                        ));
            }

            return responses;
        }

        private static ErrorOr<City> MapCity(CreateCityRequest request, int id)
        {
            var city = City.Create(
                            request.Name,
                            request.AreaCode,
                            id
                            );

            return city;
        }
        private static ErrorOr<City> MapCity(UpsertCityRequest request, int id)
        {
            var city = City.Create(
                            request.Name,
                            request.AreaCode,
                            id
                            );

            return city;
        }
        #endregion

        private CreatedAtActionResult CreatedAtGetCity(City city)
        {
            return CreatedAtAction(
                   actionName: nameof(GetCity),
                   routeValues: new { id = city.City_Id },
                   value: MapCityResponse(city));
        }
    }
}
