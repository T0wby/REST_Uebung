using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using ValidVacations.Data;
using ValidVacations.Services.Bookings;
using ValidVacation.Contracts.Booking;
using ValidVacations.Models;

namespace ValidVacations.Controllers
{
    public class BookingsController : ApiController
    {
        private readonly IBookingService _bookingService;
        private readonly AppDbContext _context;

        public BookingsController(IBookingService bookingService, AppDbContext context)
        {
            _bookingService = bookingService;
            _context = context;
        }

        [HttpPost()]
        public IActionResult CreateBooking(CreateBookingRequest request)
        {
            var booking = MapBooking(request);

            if (booking.IsError)
            {
                return Problem(booking.Errors);
            }

            // TODO: Save vacation to Database
            var createBookingResult = _bookingService.CreateBooking(booking.Value);

            return createBookingResult.Match(
               created => CreatedAtGetBooking(booking.Value),
               errors => Problem(errors));
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetBooking(Guid id)
        {
            ErrorOr<Booking> getBookingResult = _bookingService.GetBooking(id);

            return getBookingResult.Match(
                booking => Ok(MapBookingResponse(booking)),
                errors => Problem(errors));
        }


        [HttpPut("{id:guid}")]
        public IActionResult UpsertBooking(Guid id, UpsertBookingRequest request)
        {
            var booking = MapBooking(request, id);

            if (booking.IsError)
            {
                return Problem(booking.Errors);
            }

            var updateBookingResult = _bookingService.UpsertBooking(booking.Value);


            return updateBookingResult.Match(
                updated => updated.IsNewlyCreated ? CreatedAtGetBooking(booking.Value) : NoContent(),
                errors => Problem(errors)
                );
        }


        [HttpDelete("{id:guid}")]
        public IActionResult DeleteBooking(Guid id)
        {
            var deleteBookingResult = _bookingService.DeletBooking(id);


            return deleteBookingResult.Match(
                deleted => NoContent(),
                errors => Problem(errors)
                );
        }

        #region Mappings
        private static BookingResponse MapBookingResponse(Booking booking)
        {
            return new BookingResponse(
                            booking.Booking_Id,
                            booking.Title,
                            booking.DateAdded,
                            booking.DateUpdated,
                            booking.CustomerId,
                            booking.VacationId
                            );
        }

        private static ErrorOr<Booking> MapBooking(CreateBookingRequest request, Guid id = default(Guid))
        {
            var customer = Booking.Create(
                            request.title,
                            request.createdDate,
                            request.updateDate,
                            request.customerId,
                            request.vacationId,
                            id
                            );

            return customer;
        }
        private static ErrorOr<Booking> MapBooking(UpsertBookingRequest request, Guid id = default(Guid))
        {
            var customer = Booking.Create(
                            request.title,
                            request.createdDate,
                            request.updateDate,
                            request.customerId,
                            request.vacationId,
                            id
                            );

            return customer;
        }
        #endregion

        private CreatedAtActionResult CreatedAtGetBooking(Booking booking)
        {
            return CreatedAtAction(
                   actionName: nameof(GetBooking),
                   routeValues: new { id = booking.Booking_Id },
                   value: MapBookingResponse(booking));
        }
    }
}
