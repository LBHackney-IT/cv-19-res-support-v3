using System;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cv19ResSupportV3.V3.Controllers
{
    [ApiController]
    [Route("api/v3/help-requests")]
    [Produces("application/json")]
    [ApiVersion("3.0")]
    public class HelpRequestCallController : BaseController
    {
        private readonly ICreateHelpRequestCallUseCase _createHelpRequestCallUseCase;
        public HelpRequestCallController(ICreateHelpRequestCallUseCase createHelpRequestCallUseCase)
        {
            _createHelpRequestCallUseCase = createHelpRequestCallUseCase;
        }

        /// <summary>
        /// Creates a call with the values provided.
        /// </summary>
        /// <param name="id" example="123">Help request id</param>
        /// <param name="request"></param>
        /// <response code="201">Call is created</response>
        [ProducesResponseType(typeof(HelpRequestCreateResponse), StatusCodes.Status201Created)]
        [HttpPost]
        [Route("{id}/calls")]
        public IActionResult CreateHelpRequestCall([FromRoute] int id, [FromBody] CreateHelpRequestCallRequest request)
        {
            try
            {
                var createCommand = request.ToCommand();
                var callId = _createHelpRequestCallUseCase.Execute(id, createCommand);
                var result = new HelpRequestCallCreateResponse() { Id = callId };
                return Created(new Uri($"api/v3/help-requests/{id}/calls/{callId}", UriKind.Relative), result);
            }
            catch (InvalidOperationException)
            {
                return NotFound($"Record with id {id} not found");
            }
            catch (Exception e)
            {
                return BadRequest($"Call record not created. {e}");
            }
        }


    }
}
