using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TranslationManagement.Api.Commands;
using TranslationManagement.Api.Queries;

namespace TranslationManagement.Api.Controllers
{
    [ApiController, Route("api/jobs")]
    public class TranslationJobController : ControllerBase
    {
        private readonly IMediator mediator;

        public TranslationJobController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("")]
        public async Task<ActionResult<GetJobsQueryResult[]>> GetJobs()
        {
            var result = await this.mediator.Send(new GetJobsQuery());
            return Ok(result);
        }

        [HttpPost("")]
        public async Task<ActionResult<int>> CreateJob([FromBody] CreateJobCommand job)
        {
            var result = await this.mediator.Send(job);

            if (result > 0) { return Ok(result); }

            return StatusCode(400);
        }

        [HttpPost("/{customer}/upload")]
        public bool CreateJobWithFile([FromBody] IFormFile file, [FromRoute] string customer)
        {
            //var reader = new StreamReader(file.OpenReadStream());

            //// TODO: Use processors

            //var newJob = new CreateJobCommand()
            //{
            //    OriginalContent = content,
            //    TranslatedContent = "",
            //    CustomerName = customer,
            //};

            //SetPrice(newJob);

            //return CreateJob(newJob);

            return false;
        }

        [HttpPatch("{jobId}")]
        public string UpdateJobStatus([FromRoute]int jobId, int translatorId, string newStatus = "")
        {
            //_logger.LogInformation("Job status update request received: " + newStatus + " for job " + jobId.ToString() + " by translator " + translatorId);
            //if (typeof(JobStatuses).GetProperties().Count(prop => prop.Name == newStatus) == 0)
            //{
            //    return "invalid status";
            //}

            //var job = _context.TranslationJobs.Single(j => j.Id == jobId);

            //bool isInvalidStatusChange = (job.Status == JobStatuses.New && newStatus == JobStatuses.Completed) ||
            //                             job.Status == JobStatuses.Completed || newStatus == JobStatuses.New;
            //if (isInvalidStatusChange)
            //{
            //    return "invalid status change";
            //}

            //job.Status = newStatus;
            //_context.SaveChanges();
            return "updated";
        }
    }
}