using ErrorOr;
using TowbyJobs.Models;

namespace TowbyJobs.Services.Companys
{
    public interface ICompanyService
    {
        ErrorOr<Created> CreateCompany(Company company);
        ErrorOr<Company> GetCompany(int id);
        ErrorOr<List<Company>> GetCompanies(int number);
        ErrorOr<UpsertedCompanyResult> UpsertCompany(Company company);
        ErrorOr<Deleted> DeletCompany(int id);
    }
}
