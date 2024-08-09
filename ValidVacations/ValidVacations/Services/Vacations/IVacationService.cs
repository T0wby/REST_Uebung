using ErrorOr;
using ValidVacation.Contracts.Vacation;
using ValidVacations.Models;

namespace ValidVacations.Services.Vacations
{
    public interface IVacationService
    {
        ErrorOr<Created> CreateVacation(Vacation vacation);
        ErrorOr<Vacation> GetVacation(Guid id);
        ErrorOr<UpsertedVacationResult> UpsertVacation(Vacation vacation);
        ErrorOr<Deleted> DeletVacation(Guid id);
    }
}
