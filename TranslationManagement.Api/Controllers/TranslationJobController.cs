using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TranslationManagement.Api.Commands;
using TranslationManagement.Api.Queries;
using TranslationManagement.Domain.Validators;

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

        [HttpPost("/{customer}")]
        public async Task<ActionResult<int>> CreateJob([FromRoute] string customer, [FromBody] CreateJobCommand command)
        {
            command.CustomerName = customer;
            var result = await this.mediator.Send(command);
            return result > 0 ? Ok(result) : BadRequest();
        }

        [HttpPost("/{customer}/upload")]
        public async Task<ActionResult<int>> CreateJobWithFile([FromBody] IFormFile file, [FromRoute] string customer)
        {
            var command = new CreateJobFromFileCommand
            {
                Customer = customer,
                FileStream = file.OpenReadStream(),
                FileName = file.FileName
            };

            var result = await this.mediator.Send(command);
            return result > 0 ? Ok(result) : BadRequest();
        }

        [HttpPatch("{jobId}")]
        public async Task<ActionResult> UpdateJobStatus([FromRoute] int jobId, [FromBody] UpdateJobStatusCommand command)
        {
            try
            {
                command.JobId = jobId;
                var result = await this.mediator.Send(command);
                return result ? Ok() : BadRequest();
            }
            catch (ValidationException exc)
            {
                return BadRequest(exc.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}