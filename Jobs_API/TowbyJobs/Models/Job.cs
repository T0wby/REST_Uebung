using ErrorOr;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TowbyJobs.Controllers;
using TowbyJobs.ServiceErrors;

namespace TowbyJobs.Models
{
    public class Job
    {
        public const uint MinTitleLength = 1;
        public const uint MaxTitleLength = 40;

        [Key]
        public int Job_Id { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public string Contact { get; set; }
        public string ApplicationStatus { get; set; }
        public DateTime DateOfApplication { get; set; } = DateTime.Today;
        public DateTime LastTimeUpdated { get; set; } = DateTime.UtcNow;

        [ForeignKey("Company")]
        public int Company_Id { get; set; }
        public Company Company { get; set; }

        public Job()
        {
            
        }

        private Job(int jobId, string title, string url, string contact, string appStatus, DateTime dateOfApplication, int companyId)
        {
            Job_Id = jobId;
            Title = title;
            URL = url;
            Contact = contact;
            ApplicationStatus = appStatus;
            DateOfApplication = dateOfApplication;
            LastTimeUpdated = DateTime.UtcNow;
            Company_Id = companyId;
        }

        public static ErrorOr<Job> Create(string title, string url, string contact, string appStatus, DateTime dateOfApplication, int companyId, int id)
        {
            List<Error> errors = new List<Error>();

            if (title.Length < MinTitleLength || title.Length > MaxTitleLength)
            {
                errors.Add(Errors.Job.InvalidTitleLength);
            }

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Job(
                id,
                title,
                url,
                contact,
                appStatus,
                dateOfApplication,
                companyId
                );
        }
    }
}
