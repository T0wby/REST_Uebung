using ErrorOr;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TowbyJobs.ServiceErrors;

namespace TowbyJobs.Models
{
    public class Company
    {
        public const uint MinNameLength = 1;
        public const uint MaxNameLength = 40;

        [Key]
        public int Company_Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int Housenumber { get; set; } = 0;
        public string Street{ get; set; } = string.Empty;
        public DateTime LastTimeUpdated { get; set; } = DateTime.UtcNow;

        [ForeignKey("City")]
        public int City_Id { get; set; }
        public City City { get; set; }

        [ForeignKey("State")]
        public int State_Id { get; set; }
        public State State { get; set; }

        [ForeignKey("Country")]
        public int Country_Id { get; set; }
        public Country Country { get; set; }

        public List<Job> Jobs { get; set; } = new List<Job>();

        public Company()
        {
            
        }

        private Company(int company_id, string name, string email, int houseNumber, string street, int cityId, int stateId, int countryId, string phone)
        {
            Company_Id = company_id;
            Name = name;
            Email = email;
            Housenumber = houseNumber;
            Street = street;
            City_Id = cityId;
            State_Id = stateId;
            Country_Id = countryId;
            Phone = phone;
            LastTimeUpdated = DateTime.UtcNow;
        }

        public static ErrorOr<Company> Create(string name, string email, int houseNumber, string street, int cityId, int stateId, int countryId, string phone, int id)
        {
            List<Error> errors = new List<Error>();

            if (name.Length < MinNameLength || name.Length > MaxNameLength)
            {
                errors.Add(Errors.Company.InvalidNameLength);
            }

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Company(
                id,//id == Guid.Empty ? Guid.NewGuid() : id,
                name,
                email,
                houseNumber,
                street,
                cityId,
                stateId,
                countryId,
                phone
                );
        }
    }
}
