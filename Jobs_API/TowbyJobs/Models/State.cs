using ErrorOr;
using System.ComponentModel.DataAnnotations;
using TowbyJobs.ServiceErrors;

namespace TowbyJobs.Models
{
    public class State
    {
        public const uint MinNameLength = 1;
        public const uint MaxNameLength = 40;

        [Key]
        public int State_Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public DateTime LastTimeUpdated { get; set; } = DateTime.UtcNow;
        public List<Company> Companys { get; set; } = new List<Company>();


        public State()
        {
            
        }

        private State(int state_id, string name)
        {
            State_Id = state_id;
            Name = name;
            LastTimeUpdated = DateTime.UtcNow;
        }

        public static ErrorOr<State> Create(string name, int id)
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

            return new State(
                id,//id == Guid.Empty ? Guid.NewGuid() : id,
                name
                );
        }
    }
}
