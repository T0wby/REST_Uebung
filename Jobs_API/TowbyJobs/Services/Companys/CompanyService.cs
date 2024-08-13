using ErrorOr;
using Microsoft.EntityFrameworkCore;
using TowbyJobs.Data;
using TowbyJobs.Models;
using TowbyJobs.ServiceErrors;

namespace TowbyJobs.Services.Companys
{
    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext _context;

        public CompanyService(AppDbContext context)
        {
            _context = context;
        }

        public ErrorOr<Created> CreateCompany(Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();
            return Result.Created;
        }

        public ErrorOr<Deleted> DeletCompany(int id)
        {
            var company = _context.Companies.Find(id);
            if (company == null)
            {
                return Errors.Company.NotFound;
            }
            _context.Companies.Remove(company);
            _context.SaveChanges();
            return Result.Deleted;
        }

        public ErrorOr<Company> GetCompany(int id)
        {
            var company = _context.Companies.Find(id);
            return company == null ? Errors.Company.NotFound : company;
        }

        public ErrorOr<UpsertedCompanyResult> UpsertCompany(Company company)
        {
            var comp = _context.Companies.Find(company.Company_Id);
            if (comp == null) 
            { 
                CreateCompany(company); 
                return new UpsertedCompanyResult(false);
            }
            else
            {
                _context.Companies.Update(company);
                _context.SaveChanges();
                return new UpsertedCompanyResult(true);
            }

        }
    }
}
