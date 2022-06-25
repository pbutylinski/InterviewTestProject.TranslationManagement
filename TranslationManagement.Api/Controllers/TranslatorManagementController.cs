using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TranslationManagement.Api.Commands;
using TranslationManagement.Api.Queries;
using TranslationManagement.Domain.Validators;

namespace TranslationManagement.Api.Controlers
{
    [ApiController, Route("api/translators")]
    public class TranslatorManagementController : ControllerBase
    {
        private readonly IMediator mediator;

        public TranslatorManagementController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
        }

        [HttpGet("")]
        public async Task<ActionResult<GetTranslatorsQueryResult[]>> GetTranslators()
        {
            var result = await this.mediator.Send(new GetTranslatorsQuery());
            return Ok(result);
        }

        [HttpGet("/{name}")]
        public async Task<ActionResult<GetTranslatorsQueryResult>> GetTranslatorsByName([FromRoute] string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return BadRequest("Translator name cannot be empty");

            var result = await this.mediator.Send(new GetTranslatorByNameQuery { Name = name });

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost("")]
        public async Task<ActionResult<int>> AddTranslator([FromBody] CreateTranslatorCommand command)
        {
            if (command == null) return BadRequest("Request body cannot be null");

            return await this.mediator.Send(command);
        }

        [HttpPatch("{translatorId}")]
        public async Task<ActionResult> UpdateTranslatorStatus([FromRoute] int translatorId, [FromBody] UpdateTranslatorStatusCommand command)
        {
            if (command == null) return BadRequest("Request body cannot be null");

            try
            {
                command.TranslatorId = translatorId;
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