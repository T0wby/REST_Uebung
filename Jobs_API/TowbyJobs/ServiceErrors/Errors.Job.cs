using ErrorOr;

namespace TowbyJobs.ServiceErrors
{
    public static class Errors
    {
        public static class Job
        {
            public static Error InvalidTitleLength => Error.Validation(
                code: "Job.InvalidTitle",
                description: $"The Job title needs to have between {Models.Job.MinTitleLength} and {Models.Job.MaxTitleLength} Characters!");
            public static Error NotFound => Error.NotFound(
                code: "Job.NotFound",
                description: "The searched Job does not exist!");
            public static Error OutOfScope => Error.Forbidden(
                code: "Job.OutOfScope",
                description: "The entered Job count is to low !");
        }
        public static class Company
        {
            public static Error InvalidNameLength => Error.Validation(
                code: "Company.InvalidName",
                description: $"The Company name needs to have between {Models.Company.MinNameLength} and {Models.Company.MaxNameLength} Characters!");
            public static Error NotFound => Error.NotFound(
                code: "Company.NotFound",
                description: "The searched Company does not exist!");
            public static Error OutOfScope => Error.Forbidden(
                code: "Company.OutOfScope",
                description: "The entered Company count is to low!");
        }
        public static class City
        {
            public static Error InvalidNameLength => Error.Validation(
                code: "City.InvalidName",
                description: $"The City name needs to have between {Models.City.MinNameLength} and {Models.City.MaxNameLength} Characters!");
            public static Error NotFound => Error.NotFound(
                code: "City.NotFound",
                description: "The searched City does not exist!");
            public static Error OutOfScope => Error.Forbidden(
                code: "City.OutOfScope",
                description: "The entered City count is to low!");
        }
        public static class State
        {
            public static Error InvalidNameLength => Error.Validation(
                code: "State.InvalidName",
                description: $"The State name needs to have between {Models.State.MinNameLength} and {Models.State.MaxNameLength} Characters!");
            public static Error NotFound => Error.NotFound(
                code: "State.NotFound",
                description: "The searched State does not exist!");
            public static Error OutOfScope => Error.Forbidden(
                code: "State.OutOfScope",
                description: "The entered State count is to low!");
        }
        public static class Country
        {
            public static Error InvalidNameLength => Error.Validation(
                code: "Country.InvalidName",
                description: $"The Country name needs to have between {Models.Country.MinNameLength} and {Models.Country.MaxNameLength} Characters!");
            public static Error NotFound => Error.NotFound(
                code: "Country.NotFound",
                description: "The searched Country does not exist!");
            public static Error OutOfScope => Error.Forbidden(
                code: "Country.OutOfScope",
                description: "The entered Country count is to low!");
        }
    }
}
