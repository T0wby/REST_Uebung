using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using TowbyJobs.Data;
using TowbyJobs.Services.Jobs;
using TowbyJobs.Contracts.Job;
using TowbyJobs.Models;
using System.Collections.Generic;

namespace TowbyJobs.Controllers
{
    public class JobsController : ApiController
    {
        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost()]
        public IActionResult CreateJob(CreateJobRequest request)
        {
            var job = MapJob(request, 0);

            if (job.IsError)
            {
                return Problem(job.Errors);
            }

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

        [HttpGet("getJobs/{count:int}")]
        public IActionResult GetJobList(int count)
        {
            var getJobResult = _jobService.GetJobs(count);

            return getJobResult.Match(
                jobs => Ok(MapJobListResponse(jobs)),
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
                            job.Company_Id,
                            job.Title,
                            job.Contact,
                            job.URL,
                            job.ApplicationStatus,
                            DateTime.Today,
                            DateTime.UtcNow
                            );
        }
        private static List<JobResponse> MapJobListResponse(List<Job> jobs)
        {
            List<JobResponse> responses = new List<JobResponse>();

            foreach (var job in jobs)
            {
                responses.Add(
                    new JobResponse(
                            job.Job_Id,
                            job.Company_Id,
                            job.Title,
                            job.Contact,
                            job.URL,
                            job.ApplicationStatus,
                            DateTime.Today,
                            DateTime.UtcNow
                            ));
            }

            return responses;
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
