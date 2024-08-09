using ErrorOr;
using ValidVacations.Models;

namespace ValidVacations.Services.Customers
{
    public interface ICustomerService
    {
        ErrorOr<Created> CreateCustomer(Customer customer);
        ErrorOr<Customer> GetCustomer(Guid id);
        ErrorOr<UpsertedCustomerResult> UpsertCustomer(Customer customer);
        ErrorOr<Deleted> DeletCustomer(Guid id);
    }
}
