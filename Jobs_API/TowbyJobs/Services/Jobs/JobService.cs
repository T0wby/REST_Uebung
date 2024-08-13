using ErrorOr;
using TowbyJobs.Data;
using TowbyJobs.Models;
using TowbyJobs.ServiceErrors;
using TowbyJobs.Services.Companys;

namespace TowbyJobs.Services.Jobs
{
    public class JobService : IJobService
    {
        private readonly AppDbContext _context;

        public JobService(AppDbContext context)
        {
            _context = context;
        }
        public ErrorOr<Created> CreateJob(Job job)
        {
            _context.Jobs.Add(job);
            _context.SaveChanges();
            return Result.Created;
        }

        public ErrorOr<Deleted> DeletJob(int id)
        {
            var job = _context.Jobs.Find(id);
            if (job == null)
            {
                return Errors.Job.NotFound;
            }
            _context.Jobs.Remove(job);
            _context.SaveChanges();
            return Result.Deleted;
        }

        public ErrorOr<Job> GetJob(int id)
        {
            var job = _context.Jobs.Find(id);
            return job == null ? Errors.Job.NotFound : job;
        }

        public ErrorOr<UpsertedJobResult> UpsertJob(Job job)
        {
            var tempJ = _context.Jobs.Find(job.Job_Id);
            if (tempJ == null)
            {
                CreateJob(job);
                return new UpsertedJobResult(false);
            }
            else
            {
                _context.Jobs.Update(job);
                _context.SaveChanges();
                return new UpsertedJobResult(true);
            }
        }
    }
}
