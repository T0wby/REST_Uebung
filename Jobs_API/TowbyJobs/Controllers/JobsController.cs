using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using TowbyJobs.Data;
using TowbyJobs.Services.Jobs;
using TowbyJobs.Contracts.Job;
using TowbyJobs.Models;

namespace TowbyJobs.Controllers
{
    public class JobsController : ApiController
    {
        private readonly IJobService _jobService;
        private readonly AppDbContext _context;

        public JobsController(IJobService jobService, AppDbContext context)
        {
            _jobService = jobService;
            _context = context;
        }

        [HttpPost()]
        public IActionResult CreateJob(CreateJobRequest request)
        {
            var job = MapJob(request, 0);

            if (job.IsError)
            {
                return Problem(job.Errors);
            }

            // TODO: Save job to Database
            var createJobResult = _jobService.CreateJob(job.Value);

            return createJobResult.Match(
               created => CreatedAtGetJob(job.Value),
               errors => Problem(errors));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetJob(int id)
        {
            ErrorOr<Job> getJobResult = _jobService.GetJob(id);

            return getJobResult.Match(
                job => Ok(MapJobResponse(job)),
                errors => Problem(errors));
        }


        [HttpPut("{id:int}")]
        public IActionResult UpsertJob(int id, UpsertJobRequest request)
        {
            var booking = MapJob(request, id);

            if (booking.IsError)
            {
                return Problem(booking.Errors);
            }

            var updateJobResult = _jobService.UpsertJob(booking.Value);


            return updateJobResult.Match(
                updated => updated.IsNewlyCreated ? CreatedAtGetJob(booking.Value) : NoContent(),
                errors => Problem(errors)
                );
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteJob(int id)
        {
            var deleteJobResult = _jobService.DeletJob(id);


            return deleteJobResult.Match(
                deleted => NoContent(),
                errors => Problem(errors)
                );
        }

        #region Mappings
        private static JobResponse MapJobResponse(Job job)
        {
            return new JobResponse(
                            job.Job_Id,
                            job.Company.Name,
                            job.Title,
                            job.Contact,
                            job.URL,
                            "inProgress",
                            DateTime.Today,
                            DateTime.UtcNow
                            );
        }

        private static ErrorOr<Job> MapJob(CreateJobRequest request, int id)
        {
            var customer = Job.Create(
                            request.Position,
                            request.Link,
                            request.Contact,
                            request.ApplicationStatus,
                            request.DateOfApplication,
                            request.CompanyID,
                            id
                            );

            return customer;
        }
        private static ErrorOr<Job> MapJob(UpsertJobRequest request, int id)
        {
            var job = Job.Create(
                            request.Position,
                            request.Link,
                            request.Contact,
                            request.ApplicationStatus,
                            request.DateOfApplication,
                            request.CompanyID,
                            id
                            );

            return job;
        }
        #endregion

        private CreatedAtActionResult CreatedAtGetJob(Job job)
        {
            return CreatedAtAction(
                   actionName: nameof(GetJob),
                   routeValues: new { id = job.Job_Id },
                   value: MapJobResponse(job));
        }
    }
}
