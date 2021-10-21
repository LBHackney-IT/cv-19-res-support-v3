using System;
using System.Collections.Generic;
using cv19ResSupportV3.V3.Controllers;
using cv19ResSupportV3.V4.UseCase.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using cv19ResSupportV3.V4.Factories;

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
        private readonly IUpsertCallHandlerUseCase _upsertCallHandlerUseCase;

        public CallHandlersController(
             IGetCallHandlersUseCase getCallHandlersUseCase,
             IUpsertCallHandlerUseCase upsertCallHandlerUseCase)
        {
            _getCallHandlersUseCase = getCallHandlersUseCase;
            _upsertCallHandlerUseCase = upsertCallHandlerUseCase;
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

        /// <summary>
        /// Gets a call handler with the id specified.
        /// </summary>
        /// <param name="id" example="123">Call handler id</param>
        /// <response code="200">Call handler is returned</response>
        /// <response code="404">...</response>
        [ProducesResponseType(typeof(CallHandlerResponseBoundary), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCallHandler(int id)
        {
            var response = _getCallHandlersUseCase.Execute(id);
            if (response != null)
                return Ok(response);
            return (NotFound("Call handler not found"));
        }

        /// <summary>
        /// Creates a call handler with the values provided.
        /// </summary>
        /// <response code="201">Call handler is created</response>
        /// <response code="400">...</response>
        [ProducesResponseType(typeof(CreatedResult), StatusCodes.Status201Created)]
        [HttpPut]
        public IActionResult PutCallHandler(PutCallHandlerRequestBoundary request)
        {
            var response = _upsertCallHandlerUseCase.Execute(request.ToDomain());

            if (response != null)
                return Created(new Uri($"api/v4/call-handlers/{response.Id}", UriKind.Relative), response);

            return (BadRequest("Call handler not created"));
        }

        /// <summary>
        /// Creates a call handler with the values provided.
        /// </summary>
        /// <response code="201">Call handler is created</response>
        /// <response code="400">...</response>
        [ProducesResponseType(typeof(CreatedResult), StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult CreateCallHandler(CreateCallHandlerRequestBoundary request)
        {
            if (request == null) return (BadRequest("Call handler not created"));

            var response = _upsertCallHandlerUseCase.Execute(request.ToDomain());

            return Created(new Uri($"api/v4/call-handlers/{response.Id}", UriKind.Relative), response);
        }
    }
}
