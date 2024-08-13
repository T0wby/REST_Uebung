using ErrorOr;
using System.ComponentModel.DataAnnotations;
using TowbyJobs.ServiceErrors;

namespace TowbyJobs.Models
{
    public class Country
    {
        public const uint MinNameLength = 1;
        public const uint MaxNameLength = 40;

        [Key]
        public int Country_Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public DateTime LastTimeUpdated { get; set; } = DateTime.UtcNow;
        public List<Company> Companys { get; set; } = new List<Company>();

        public Country()
        {
            
        }

        private Country(int country_id, string name, string code)
        {
            Country_Id = country_id;
            Name = name;
            Code = code;
            LastTimeUpdated = DateTime.UtcNow;
        }

        public static ErrorOr<Country> Create(string name, string code, int id)
        {
            List<Error> errors = new List<Error>();

            if (name.Length < MinNameLength || name.Length > MaxNameLength)
            {
                errors.Add(Errors.Country.InvalidNameLength);
            }

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Country(
                id,//id == Guid.Empty ? Guid.NewGuid() : id,
                name,
                code
                );
        }
    }
}
