using ErrorOr;
using ValidVacations.Models;

namespace ValidVacations.Services.Bookings
{
    public class BookingService : IBookingService
    {
        public ErrorOr<Created> CreateBooking(Booking customer)
        {
            throw new NotImplementedException();
        }

        public ErrorOr<Deleted> DeletBooking(Guid id)
        {
            throw new NotImplementedException();
        }

        public ErrorOr<Booking> GetBooking(Guid id)
        {
            throw new NotImplementedException();
        }

        public ErrorOr<UpsertedBookingResult> UpsertBooking(Booking customer)
        {
            throw new NotImplementedException();
        }
    }
}
