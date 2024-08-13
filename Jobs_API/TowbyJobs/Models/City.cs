using ErrorOr;
using static TowbyJobs.ServiceErrors.Errors;
using System.Numerics;
using TowbyJobs.ServiceErrors;
using System.ComponentModel.DataAnnotations;

namespace TowbyJobs.Models
{
    public class City
    {
        public const uint MinNameLength = 1;
        public const uint MaxNameLength = 40;

        [Key]
        public int City_Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string AreaCode { get; set; } = string.Empty;
        public DateTime LastTimeUpdated { get; set; } = DateTime.UtcNow;
        public List<Company> Companys { get; set; } = new List<Company>();

        public City()
        {
            
        }

        private City(int city_id, string name, string areacode)
        {
            City_Id = city_id;
            Name = name;
            AreaCode = areacode;
            LastTimeUpdated = DateTime.UtcNow;
        }

        public static ErrorOr<City> Create(string name, string areacode, int id)
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

            return new City(
                id,
                name,
                areacode
                );
        }
    }
}
