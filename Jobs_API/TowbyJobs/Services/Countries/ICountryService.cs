﻿using ErrorOr;
using TowbyJobs.Models;

namespace TowbyJobs.Services.Countries
{
    public interface ICountryService
    {
        ErrorOr<Created> CreateCountry(Country country);
        ErrorOr<Country> GetCountry(int id);
        ErrorOr<List<Country>> GetCountries(int number);
        ErrorOr<UpsertedCountryResult> UpsertCountry(Country country);
        ErrorOr<Deleted> DeletCountry(int id);
    }
}
