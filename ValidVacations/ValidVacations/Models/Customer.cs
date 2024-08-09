using ErrorOr;
using ValidVacations.ServiceErrors;

namespace ValidVacations.Models
{
    public class Customer : BaseModel
    {
        public const uint MinNameLength = 1;
        public const uint MaxNameLength = 40;

        public Guid Customer_Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public int Housenumber { get; }
        public string Street{ get; }
        public string City { get; }
        public string AreaCode { get; }
        public string State { get; }
        public string Country { get; }
        public string Phone { get; }

        public List<Booking> Bookings { get; }

        private Customer(Guid customer_id, string firstName, string lastName, string email, int houseNumber, string street, string city, string areaCode, string state, string country, string phone)
        {
            Customer_Id = customer_id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Housenumber = houseNumber;
            Street = street;
            City = city;
            AreaCode = areaCode;
            State = state;
            Country = country;
            Phone = phone;
            Bookings = new List<Booking>();
        }

        public static ErrorOr<Customer> Create(string firstName, string lastName, string email, int houseNumber, string street, string city, string areaCode, string state, string country, string phone, Guid id = default(Guid))
        {
            List<Error> errors = new List<Error>();

            if (firstName.Length < MinNameLength || firstName.Length > MaxNameLength)
            {
                errors.Add(Errors.Customer.InvalidFirstNameLength);
            }
            if (lastName.Length < MinNameLength || lastName.Length > MaxNameLength)
            {
                errors.Add(Errors.Customer.InvalidLastNameLength);
            }

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Customer(
                id == Guid.Empty ? Guid.NewGuid() : id,
                firstName,
                lastName,
                email,
                houseNumber,
                street,
                city,
                areaCode,
                state,
                country,
                phone
                );
        }
    }
}
