using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using TowbyJobs.Contracts.Company;
using TowbyJobs.Data;
using TowbyJobs.Models;
using TowbyJobs.Services.Companys;

namespace TowbyJobs.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost()]
        public IActionResult CreateCompany(CreateCompanyRequest request)
        {
            var company = MapCompany(request, 0);

            if (company.IsError)
            {
                return Problem(company.Errors);
            }

            var createCompanyResult = _companyService.CreateCompany(company.Value);

            return createCompanyResult.Match(
               created => CreatedAtGetCompany(company.Value),
               errors => Problem(errors));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetCompany(int id)
        {
            ErrorOr<Company> getCompanyResult = _companyService.GetCompany(id);

            return getCompanyResult.Match(
                company => Ok(MapCompanyResponse(company)),
                errors => Problem(errors));
        }


        [HttpPut("{id:int}")]
        public IActionResult UpsertCompany(int id, UpsertCompanyRequest request)
        {
            var customer = MapCompany(request, id);

            if (customer.IsError)
            {
                return Problem(customer.Errors);
            }

            var updateCompanyResult = _companyService.UpsertCompany(customer.Value);


            return updateCompanyResult.Match(
                updated => updated.IsNewlyCreated ? CreatedAtGetCompany(customer.Value) : NoContent(),
                errors => Problem(errors)
                );
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteCompany(int id)
        {
            var deleteCustomerResult = _companyService.DeletCompany(id);


            return deleteCustomerResult.Match(
                deleted => NoContent(),
                errors => Problem(errors)
                );
        }

        #region Mappings
        private static CompanyResponse MapCompanyResponse(Company company)
        {
            return new CompanyResponse(
                            company.Company_Id,
                            company.Name,
                            company.Email,
                            company.Housenumber,
                            company.Street,
                            company.City_Id,
                            company.State_Id,
                            company.Country_Id,
                            company.Phone,
                            DateTime.UtcNow
                            );
        }

        private static ErrorOr<Company> MapCompany(CreateCompanyRequest request, int id)
        {
            var customer = Company.Create(
                            request.Name,
                            request.Email,
                            request.HouseNumber,
                            request.Street,
                            request.City_Id,
                            request.State_Id,
                            request.Country_Id,
                            request.Phone,
                            id
                            );

            return customer;
        }
        private static ErrorOr<Company> MapCompany(UpsertCompanyRequest request, int id)
        {
            var customer = Company.Create(
                            request.Name,
                            request.Email,
                            request.HouseNumber,
                            request.Street,
                            request.City_Id,
                            request.State_Id,
                            request.Country_Id,
                            request.Phone,
                            id
                            );

            return customer;
        }
        #endregion

        private CreatedAtActionResult CreatedAtGetCompany(Company customer)
        {
            return CreatedAtAction(
                   actionName: nameof(GetCompany),
                   routeValues: new { id = customer.Company_Id },
                   value: MapCompanyResponse(customer));
        }
    }
}

