using ErrorOr;
using ValidVacations.ServiceErrors;

namespace ValidVacations.Models
{
    public class Vacation : BaseModel
    {
        public const uint MinTitleLength = 3;
        public const uint MaxTitleLength = 80;
        public const uint MinDescLength = 20;
        public const uint MaxDescLength = 300;

        public Guid? Vacation_Id { get; }
        public string? Title { get; }
        public string? Description { get; }
        public DateTime StartDate { get;}
        public DateTime EndDate { get; }
        public DateTime LastModifiedDateTime { get; }
        public uint Length { get; }
        public uint Price { get; }

        public List<Booking> Bookings { get; }

        private Vacation(Guid? vacationId, string title, string description, DateTime startDate, DateTime endDate, DateTime lastModifiedDateTime, uint length, uint price)
        {
            Vacation_Id = vacationId;
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            LastModifiedDateTime = lastModifiedDateTime;
            Length = length;
            Price = price;
            Bookings = new List<Booking>();
        }

        public static ErrorOr<Vacation> Create(string title, string description, DateTime startDate, DateTime endDate, DateTime lastModifiedDateTime, uint length, uint price, Guid? id = null)
        {
            List<Error> errors = new List<Error>();

            if (title.Length < MinTitleLength || title.Length > MaxTitleLength)
            {
                errors.Add(Errors.Vacation.InvalidTitleLength);
            }
            if (description.Length < MinDescLength || description.Length > MaxDescLength)
            {
                errors.Add(Errors.Vacation.InvalidDescLength);
            }

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Vacation(
                id == null ? Guid.NewGuid() : id,
                title, 
                description, 
                startDate, 
                endDate,
                lastModifiedDateTime,
                length,
                price
                );
        }
    }
}
