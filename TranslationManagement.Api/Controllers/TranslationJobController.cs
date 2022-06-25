using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TranslationManagement.Api.Commands;
using TranslationManagement.Api.Queries;
using TranslationManagement.Domain.Validators;
using TranslationManagement.FileProcessors.Exceptions;

namespace TranslationManagement.Api.Controllers
{
    [ApiController, Route("api/jobs")]
    public class TranslationJobController : ControllerBase
    {
        private readonly IMediator mediator;

        public TranslationJobController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
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
            if (command == null) return BadRequest("Request body cannot be null");

            command.CustomerName = customer;
            var result = await this.mediator.Send(command);
            return result > 0 ? Ok(result) : BadRequest();
        }

        [HttpPost("/{customer}/upload")]
        public async Task<ActionResult<int>> CreateJobWithFile(IFormFile file, [FromRoute] string customer)
        {
            if (file == null) return BadRequest("File cannot be empty");

            var command = new CreateJobFromFileCommand
            {
                Customer = customer,
                FileStream = file.OpenReadStream(),
                FileName = file.FileName
            };

            try
            {
                var result = await this.mediator.Send(command); 
                return result > 0 ? Ok(result) : BadRequest();
            }
            catch (FileProcessingException exc)
            {
                return BadRequest(exc.Message);
            }
            catch (UnsupportedFileException exc)
            {
                return BadRequest(exc.Message);
            }   
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPatch("{jobId}")]
        public async Task<ActionResult> UpdateJobStatus([FromRoute] int jobId, [FromBody] UpdateJobStatusCommand command)
        {
            if (command == null) return BadRequest("Request body cannot be null");

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