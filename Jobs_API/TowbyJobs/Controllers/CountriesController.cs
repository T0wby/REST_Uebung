using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using TowbyJobs.Contracts.Country;
using TowbyJobs.Models;
using TowbyJobs.Services.Countries;

namespace TowbyJobs.Controllers
{
    public class CountriesController : ApiController
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpPost()]
        public IActionResult CreateCountry(CreateCountryRequest request)
        {
            var country = MapCountry(request, 0);

            if (country.IsError)
            {
                return Problem(country.Errors);
            }

            var createCountryResult = _countryService.CreateCountry(country.Value);

            return createCountryResult.Match(
               created => CreatedAtGetCountry(country.Value),
               errors => Problem(errors));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetCountry(int id)
        {
            ErrorOr<Country> getCountryResult = _countryService.GetCountry(id);

            return getCountryResult.Match(
                job => Ok(MapCountryResponse(job)),
                errors => Problem(errors));
        }

        [HttpGet("getCountries/{count:int}")]
        public IActionResult GetCountryList(int count)
        {
            var getCountryResult = _countryService.GetCountries(count);

            return getCountryResult.Match(
                countries => Ok(MapCountryListResponse(countries)),
                errors => Problem(errors));
        }


        [HttpPut("{id:int}")]
        public IActionResult UpsertCountry(int id, UpsertCountryRequest request)
        {
            var country = MapCountry(request, id);

            if (country.IsError)
            {
                return Problem(country.Errors);
            }

            var updateJobResult = _countryService.UpsertCountry(country.Value);


            return updateJobResult.Match(
                updated => updated.IsNewlyCreated ? CreatedAtGetCountry(country.Value) : NoContent(),
                errors => Problem(errors)
                );
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteCountry(int id)
        {
            var deleteCountryResult = _countryService.DeletCountry(id);


            return deleteCountryResult.Match(
                deleted => NoContent(),
                errors => Problem(errors)
                );
        }

        #region Mappings
        private static CountryResponse MapCountryResponse(Country country)
        {
            return new CountryResponse(
                            country.Country_Id,
                            country.Name,
                            country.Code,
                            country.LastTimeUpdated
                            );
        }
        private static List<CountryResponse> MapCountryListResponse(List<Country> countries)
        {
            List<CountryResponse> responses = new List<CountryResponse>();

            foreach (var country in countries)
            {
                responses.Add(
                new CountryResponse(
                    country.Country_Id,
                    country.Name,
                    country.Code,
                    country.LastTimeUpdated
                ));
            }

            return responses;
        }

        private static ErrorOr<Country> MapCountry(CreateCountryRequest request, int id)
        {
            var country = Country.Create(
                            request.Name,
                            request.Code,
                            id
                            );

            return country;
        }
        private static ErrorOr<Country> MapCountry(UpsertCountryRequest request, int id)
        {
            var country = Country.Create(
                            request.Name,
                            request.Code,
                            id
                            );

            return country;
        }
        #endregion

        private CreatedAtActionResult CreatedAtGetCountry(Country country)
        {
            return CreatedAtAction(
                   actionName: nameof(GetCountry),
                   routeValues: new { id = country.Country_Id },
                   value: MapCountryResponse(country));
        }
    }
}
