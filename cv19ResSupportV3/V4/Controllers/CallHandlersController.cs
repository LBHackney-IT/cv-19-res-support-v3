using System;
using System.Collections.Generic;
using cv19ResSupportV3.V3.Controllers;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V4.UseCase.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using cv19ResSupportV3.V4.UseCase.Interfaces;

namespace cv19ResSupportV3.V4.Controllers
{
    [ApiController]
    [Route("api/v4/call-handlers")]
    [Produces("application/json")]
    // Check service api version information
    [ApiVersion("3.0")]

    public class CallHandlersController : BaseController
    {
        private readonly IGetCallHandlersUseCase _getCallHandlersUseCase;

        public CallHandlersController(
             IGetCallHandlersUseCase getCallHandlersUseCase)
        {
            _getCallHandlersUseCase = getCallHandlersUseCase;
        }


        /// <summary>
        /// Gets all call handlers.
        /// </summary>
        /// <response code="200">Call handlers are returned</response>
        [ProducesResponseType(typeof(List<CallHandlerResponseBoundary>), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetCallHandlers()
        {
            var response = _getCallHandlersUseCase.Execute();
            return Ok(response);
        }
    }
}
