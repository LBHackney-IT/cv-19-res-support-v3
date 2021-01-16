using System.Collections.Generic;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.UseCase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cv19ResSupportV3.V3.Controllers
{
    [ApiController]
    [Route("api/v3/help-requests/callbacks")]
    [Produces("application/json")]
    [ApiVersion("3.0")]
    public class CallbacksController : BaseController
    {
        private readonly IGetCallbacksUseCase _getCallbacksUseCase;
        public CallbacksController(IGetCallbacksUseCase getCallbacksUseCase)
        {
            _getCallbacksUseCase = getCallbacksUseCase;
        }

        /// <summary>
        /// Returns a list of help requests requiring a callback.
        /// </summary>
        /// <response code="200">A list of 0 or more callbacks returned.</response>
        [ProducesResponseType(typeof(List<HelpRequestResponse>), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetCallbacks([FromQuery] CallbackRequestParams requestParams)
        {
            var command = requestParams.ToCommand();
            var result = _getCallbacksUseCase.Execute(command);
            return Ok(result.ToResponse());
        }

    }
}
