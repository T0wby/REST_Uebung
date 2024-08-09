using ErrorOr;

namespace ValidVacations.ServiceErrors
{
    public static class Errors
    {
        public static class Vacation
        {
            public static Error InvalidTitleLength => Error.Validation(
                code: "Vacation.InvalidTitle",
                description: $"The Vacation title needs to have between {Models.Vacation.MinTitleLength} and {Models.Vacation.MaxTitleLength} Characters!");
            public static Error InvalidDescLength => Error.Validation(
                code: "Vacation.InvalidDescription",
                description: $"The Vacation description needs to have between {Models.Vacation.MinDescLength} and {Models.Vacation.MaxDescLength} Characters!");
            public static Error NotFound => Error.NotFound(
                code: "Vacation.NotFound",
                description: "The searched Vacation does not exist!");
        }
        public static class Customer
        {
            public static Error InvalidFirstNameLength => Error.Validation(
                code: "Customer.InvalidFirstNameLength",
                description: $"The Customer first name needs to have between {Models.Customer.MinNameLength} and {Models.Customer.MaxNameLength} Characters!");
            public static Error InvalidLastNameLength => Error.Validation(
                code: "Customer.InvalidLastNameLength",
                description: $"The Customer last name needs to have between {Models.Customer.MinNameLength} and {Models.Customer.MaxNameLength} Characters!");
            public static Error NotFound => Error.NotFound(
                code: "Customer.NotFound",
                description: "The searched Customer does not exist!");
        }
        public static class Booking
        {
            public static Error InvalidTitleLength => Error.Validation(
                code: "Booking.InvalidTitle",
                description: $"The Booking title needs to have between {Models.Booking.MinTitleLength} and {Models.Booking.MaxTitleLength} Characters!");
            public static Error NotFound => Error.NotFound(
                code: "Booking.NotFound",
                description: "The searched Booking does not exist!");
        }
    }
}
