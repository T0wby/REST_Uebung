using ErrorOr;
using System.ComponentModel.DataAnnotations.Schema;
using ValidVacations.Controllers;
using ValidVacations.ServiceErrors;

namespace ValidVacations.Models
{
    public class Booking : BaseModel
    {
        public const uint MinTitleLength = 1;
        public const uint MaxTitleLength = 40;

        public Guid Booking_Id { get; }
        public string Title { get; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; }
        public Customer Customer { get; }

        [ForeignKey("Vacation")]
        public Guid VacationId { get; }
        public Vacation Vacation { get; }

        private Booking(Guid bookingId, string title, DateTime createdDate, DateTime updatedDate, Guid customerId, Guid vacationId)
        {
            Booking_Id = bookingId;
            Title = title;
            DateAdded = createdDate;
            DateUpdated = updatedDate;
            CustomerId = customerId;
            VacationId = vacationId;
        }

        public static ErrorOr<Booking> Create(string title, DateTime createdDate, DateTime updatedDate, Guid customerId, Guid vacationId, Guid id = default(Guid))
        {
            List<Error> errors = new List<Error>();

            if (title.Length < MinTitleLength || title.Length > MaxTitleLength)
            {
                errors.Add(Errors.Booking.InvalidTitleLength);
            }

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Booking(
                id == Guid.Empty ? Guid.NewGuid() : id,
                title,
                createdDate,
                updatedDate,
                customerId,
                vacationId
                );
        }
    }
}
