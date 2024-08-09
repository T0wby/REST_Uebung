using ErrorOr;
using ValidVacations.Models;

namespace ValidVacations.Services.Bookings
{
    public interface IBookingService
    {
        ErrorOr<Created> CreateBooking(Booking booking);
        ErrorOr<Booking> GetBooking(Guid id);
        ErrorOr<UpsertedBookingResult> UpsertBooking(Booking booking);
        ErrorOr<Deleted> DeletBooking(Guid id);
    }
}
