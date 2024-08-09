using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using ValidVacation.Contracts.Customer;
using ValidVacations.Data;
using ValidVacations.Models;
using ValidVacations.Services.Customers;

namespace ValidVacations.Controllers
{
    public class CustomersController : ApiController
    {
        private readonly ICustomerService _customerService;
        private readonly AppDbContext _context;

        public CustomersController(ICustomerService customerService, AppDbContext context)
        {
            _customerService = customerService;
            _context = context;
        }

        [HttpPost()]
        public IActionResult CreateCustomer(CreateCustomerRequest request)
        {
            var customer = MapCustomer(request);

            if (customer.IsError)
            {
                return Problem(customer.Errors);
            }

            // TODO: Save vacation to Database
            var createVacationResult = _customerService.CreateCustomer(customer.Value);

            return createVacationResult.Match(
               created => CreatedAtGetCustomer(customer.Value),
               errors => Problem(errors));
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetCustomer(Guid id)
        {
            ErrorOr<Customer> getCustomerResult = _customerService.GetCustomer(id);

            return getCustomerResult.Match(
                customer => Ok(MapCustomerResponse(customer)),
                errors => Problem(errors));
        }


        [HttpPut("{id:guid}")]
        public IActionResult UpsertCustomer(Guid id, UpsertCustomerRequest request)
        {
            var customer = MapCustomer(request, id);

            if (customer.IsError)
            {
                return Problem(customer.Errors);
            }

            var updateVacationResult = _customerService.UpsertCustomer(customer.Value);


            return updateVacationResult.Match(
                updated => updated.IsNewlyCreated ? CreatedAtGetCustomer(customer.Value) : NoContent(),
                errors => Problem(errors)
                );
        }


        [HttpDelete("{id:guid}")]
        public IActionResult DeleteCustomer(Guid id)
        {
            var deleteCustomerResult = _customerService.DeletCustomer(id);


            return deleteCustomerResult.Match(
                deleted => NoContent(),
                errors => Problem(errors)
                );
        }

        #region Mappings
        private static CustomerResponse MapCustomerResponse(Customer customer)
        {
            return new CustomerResponse(
                            customer.Customer_Id,
                            customer.FirstName,
                            customer.LastName,
                            customer.Email,
                            customer.Housenumber,
                            customer.Street,
                            customer.City,
                            customer.AreaCode,
                            customer.State,
                            customer.Country,
                            customer.Phone
                            );
        }

        private static ErrorOr<Customer> MapCustomer(CreateCustomerRequest request, Guid id = default(Guid))
        {
            var customer = Customer.Create(
                            request.firstName,
                            request.lastName,
                            request.email,
                            request.houseNumber,
                            request.street,
                            request.city,
                            request.areaCode,
                            request.state,
                            request.country,
                            request.phone,
                            id
                            );

            return customer;
        }
        private static ErrorOr<Customer> MapCustomer(UpsertCustomerRequest request, Guid id = default(Guid))
        {
            var customer = Customer.Create(
                            request.firstName,
                            request.lastName,
                            request.email,
                            request.houseNumber,
                            request.street,
                            request.city,
                            request.areaCode,
                            request.state,
                            request.country,
                            request.phone,
                            id
                            );

            return customer;
        }
        #endregion

        private CreatedAtActionResult CreatedAtGetCustomer(Customer customer)
        {
            return CreatedAtAction(
                   actionName: nameof(GetCustomer),
                   routeValues: new { id = customer.Customer_Id },
                   value: MapCustomerResponse(customer));
        }
    }
}

