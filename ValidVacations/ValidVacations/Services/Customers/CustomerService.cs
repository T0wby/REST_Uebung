using ErrorOr;
using ValidVacations.Models;

namespace ValidVacations.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        public ErrorOr<Created> CreateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public ErrorOr<Deleted> DeletCustomer(Guid id)
        {
            throw new NotImplementedException();
        }

        public ErrorOr<Customer> GetCustomer(Guid id)
        {
            throw new NotImplementedException();
        }

        public ErrorOr<UpsertedCustomerResult> UpsertCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
