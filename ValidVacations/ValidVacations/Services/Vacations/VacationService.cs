using ErrorOr;
using System;
using ValidVacations.Models;
using ValidVacations.ServiceErrors;
using static ValidVacations.ServiceErrors.Errors;

namespace ValidVacations.Services.Vacations
{
    public class VacationService : IVacationService
    {
        // Temp
        private static readonly Dictionary<Guid?, Models.Vacation> _vacations = new Dictionary<Guid?, Models.Vacation>();
        //private readonly AppDbContext _dbContext;

        public ErrorOr<Created> CreateVacation(Models.Vacation vacation)
        {
            //TODO: Use EF Core to store in database
            _vacations.Add(vacation.Vacation_Id, vacation);
            return Result.Created;
        }

        public ErrorOr<Deleted> DeletVacation(Guid id)
        {
            if (_vacations.ContainsKey(id))
            {
                _vacations.Remove(id);
                return Result.Deleted;
            }
            return Errors.Vacation.NotFound;
        }

        public ErrorOr<Models.Vacation> GetVacation(Guid id)
        {
            if (_vacations.TryGetValue(id, out var vacation))
            {
                return vacation;
            }

            return Errors.Vacation.NotFound;
        }

        public ErrorOr<UpsertedVacationResult> UpsertVacation(Models.Vacation vacation)
        {
            var isNewlyCreated = !_vacations.ContainsKey(vacation.Vacation_Id);

            if (isNewlyCreated)
                _vacations.Add(vacation.Vacation_Id, vacation);
            else
                _vacations[vacation.Vacation_Id] = vacation;

            return new UpsertedVacationResult(isNewlyCreated);
        }
    }
}
