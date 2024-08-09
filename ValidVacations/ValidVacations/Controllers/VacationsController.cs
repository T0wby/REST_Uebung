using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ValidVacation.Contracts.Vacation;
using ValidVacations.Data;
using ValidVacations.Models;
using ValidVacations.ServiceErrors;
using ValidVacations.Services.Vacations;

namespace ValidVacations.Controllers
{
    
    public class VacationsController : ApiController
    {
        private readonly IVacationService _vacationService;
        private readonly AppDbContext _context;

        public VacationsController(IVacationService vacationService, AppDbContext context)
        {
            _vacationService = vacationService;
            _context = context;
        }

        [HttpPost()]
        public IActionResult CreateVacation(CreateVacationRequest request)
        {
            var vacation = MapVacation(request);

            if (vacation.IsError)
            {
                return Problem(vacation.Errors);
            }

            // TODO: Save vacation to Database
            var createVacationResult = _vacationService.CreateVacation(vacation.Value);

            return createVacationResult.Match(
               created => CreatedAtGetVacation(vacation.Value),
               errors => Problem(errors));
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetVacation(Guid id)
        {
            ErrorOr<Vacation> getVacationResult = _vacationService.GetVacation(id);

            return getVacationResult.Match(
                vacation => Ok(MapVacationResponse(vacation)), 
                errors => Problem(errors));
        }


        [HttpPut("{id:guid}")]
        public IActionResult UpsertVacation(Guid id, UpsertVacationRequest request)
        {
            var vacation = MapVacation(request, id);

            if (vacation.IsError)
            {
                return Problem(vacation.Errors);
            }

            var updateVacationResult = _vacationService.UpsertVacation(vacation.Value);


            return updateVacationResult.Match(
                updated => updated.IsNewlyCreated ? CreatedAtGetVacation(vacation.Value) : NoContent(),
                errors => Problem(errors)
                );
        }


        [HttpDelete("{id:guid}")]
        public IActionResult DeleteVacation(Guid id)
        {
            var deleteVacationResult = _vacationService.DeletVacation(id);

            
            return deleteVacationResult.Match(
                deleted => NoContent(),
                errors => Problem(errors)
                );
        }

        #region Mappings
        private static VacationResponse MapVacationResponse(Vacation vacation)
        {
            return new VacationResponse(
                            vacation.Vacation_Id,
                            vacation.Title,
                            vacation.Description,
                            vacation.StartDate,
                            vacation.EndDate,
                            vacation.LastModifiedDateTime,
                            vacation.Length,
                            vacation.Price
                            );
        }

        private static ErrorOr<Vacation> MapVacation(CreateVacationRequest request, Guid? id = null)
        {
            var vacation = Vacation.Create(
                            request.title,
                            request.description,
                            request.startDate,
                            request.endDate,
                            DateTime.UtcNow,
                            request.length,
                            request.price,
                            id
                            );

            return vacation;
        }
        private static ErrorOr<Vacation> MapVacation(UpsertVacationRequest request, Guid? id = null)
        {
            var vacation = Vacation.Create(
                            request.title,
                            request.description,
                            request.startDate,
                            request.endDate,
                            DateTime.UtcNow,
                            request.length,
                            request.price,
                            id
                            );

            return vacation;
        }
        #endregion

        private CreatedAtActionResult CreatedAtGetVacation(Vacation vacation)
        {
            return CreatedAtAction(
                   actionName: nameof(GetVacation),
                   routeValues: new { id = vacation.Vacation_Id },
                   value: MapVacationResponse(vacation));
        }
    }
    

    
}
