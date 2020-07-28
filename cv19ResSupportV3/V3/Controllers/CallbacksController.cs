using System;
using System.Collections.Generic;
using System.Diagnostics;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.UseCase;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cv19ResSupportV3.V3.Controllers
{
    [ApiController]
    //TODO: Rename to match the APIs endpoint
    [Route("api/v3/help-requests/callbacks")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    //TODO: rename class to match the API name
    public class CallbacksController : BaseController
    {
        private readonly IGetCallbacksUseCase _getCallbacksUseCase;
        public CallbacksController(IGetCallbacksUseCase getCallbacksUseCase)
        {
            _getCallbacksUseCase = getCallbacksUseCase;
        }

        //TODO: add xml comments containing information that will be included in the auto generated swagger docs (https://github.com/LBHackney-IT/lbh-base-api/wiki/Controllers-and-Response-Objects)
        /// <summary>
        /// ...
        /// </summary>
        /// <response code="200">...</response>
        [ProducesResponseType(typeof(HelpRequestResponseList), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetCallbacks()
        {
            var result = _getCallbacksUseCase.Execute();
            return Ok(result);
        }

    }
}
