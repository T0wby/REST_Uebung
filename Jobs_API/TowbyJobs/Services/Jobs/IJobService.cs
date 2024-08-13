using ErrorOr;
using TowbyJobs.Models;

namespace TowbyJobs.Services.Jobs
{
    public interface IJobService
    {
        ErrorOr<Created> CreateJob(Job job);
        ErrorOr<Job> GetJob(int id);
        ErrorOr<UpsertedJobResult> UpsertJob(Job job);
        ErrorOr<Deleted> DeletJob(int id);
    }
}
