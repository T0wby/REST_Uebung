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

            if (job == null) return Errors.Job.NotFound;

            var company = _context.Companies.Find(job.Company_Id);

            if (company != null)
            {
                job.Company = company;
            }

            return job;
        }

        public ErrorOr<List<Job>> GetJobs(int number)
        {
            if (number <= 0)
            {
                return Errors.Job.OutOfScope;
            }

            if(number > _context.Jobs.Count()) number = _context.Jobs.Count();

            var jobs = _context.Jobs.Take(number).ToList();

            foreach (var job in jobs) 
            {
                if (job == null) continue;
                var company = _context.Companies.Find(job.Company_Id);

                if (company != null)
                {
                    job.Company = company;
                }
            }

            return jobs;
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
