using System;
using System.Collections.Generic;
using cv19ResSupportV3.V3.Controllers;
using cv19ResSupportV3.V4.Boundary.Requests;
using cv19ResSupportV3.V4.Boundary.Response;
using cv19ResSupportV3.V4.Helpers;
using cv19ResSupportV3.V4.UseCase.Interface;
using cv19ResSupportV3.V4.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cv19ResSupportV3.V4.Controllers
{
    [ApiController]
    [Route("api/v4/residents/{id}/help-requests")]
    [Produces("application/json")]
    // Check service api version information
    [ApiVersion("3.0")]

    public class ResidentHelpRequestsController : BaseController
    {
        private readonly IGetResidentHelpRequestsUseCase _getResidentHelpRequestsUseCase;
        private readonly IGetResidentHelpRequestUseCase _getResidentHelpRequestUseCase;
        private readonly ICreateResidentHelpRequestUseCase _createResidentHelpRequestUseCase;
        private readonly IPatchResidentHelpRequestUseCase _patchResidentHelpRequestUseCase;

        public ResidentHelpRequestsController(IGetResidentHelpRequestsUseCase getResidentHelpRequestsUseCase,
            IGetResidentHelpRequestUseCase getResidentHelpRequestUseCase,
            ICreateResidentHelpRequestUseCase createResidentHelpRequestUseCase,
            IPatchResidentHelpRequestUseCase patchResidentHelpRequestUseCase)
        {
            _getResidentHelpRequestsUseCase = getResidentHelpRequestsUseCase;
            _getResidentHelpRequestUseCase = getResidentHelpRequestUseCase;
            _createResidentHelpRequestUseCase = createResidentHelpRequestUseCase;
            _patchResidentHelpRequestUseCase = patchResidentHelpRequestUseCase;
        }


        /// <summary>
        /// Creates a resident help request with the values provided.
        /// </summary>
        /// <param name="id" example="123">Resident id</param>
        /// <param name="request"></param>
        /// <response code="201">Help request is created</response>
        [ProducesResponseType(typeof(ResidentHelpRequestResponse), StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult CreateResidentHelpRequest([FromRoute] int id, [FromBody] ResidentHelpRequestRequest request)
        {
            var response = _createResidentHelpRequestUseCase.Execute(id, request);
            return Created(new Uri($"api/v4/residents/{id}/help-requests/{response}", UriKind.Relative), response);
        }

        /// <summary>
        /// Gets a resident help request with the id specified.
        /// </summary>
        /// <param name="id" example="123">Resident id</param>
        /// <param name="helpRequestId"></param>
        /// <param name="includeType"></param>
        /// <response code="200">Help request is returned</response>
        /// <response code="404">...</response>
        [ProducesResponseType(typeof(ResidentHelpRequestResponse), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{help-request-id}")]
        public IActionResult GetResidentHelpRequest(
            [FromRoute(Name = "id")] int id,
            [FromRoute(Name = "help-request-id")] int helpRequestId,
            string includeType)
        {
            var response = _getResidentHelpRequestUseCase.Execute(id, helpRequestId, DataFilteringHelpers.GetExcludedHelpTypes(includeType));
            if (response != null)
                return Ok(response);
            return (NotFound("Resident or help request not found"));
        }

        /// <summary>
        /// Gets a collection of help requests for a resident with the id specified.
        /// </summary>
        /// <param name="id" example="123">Resident id</param>
        /// <param name="includeType"></param>
        /// <response code="200">List of help requests is returned</response>
        /// <response code="404">...</response>
        [ProducesResponseType(typeof(List<ResidentHelpRequestResponse>), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetResidentHelpRequests(int id, string includeType)
        {
            var response = _getResidentHelpRequestsUseCase.Execute(id, DataFilteringHelpers.GetExcludedHelpTypes(includeType));

            if (response != null)
                return Ok(response);

            return (NotFound("Resident not found"));
        }

        /// <summary>
        /// Updates a help request with the id specified.
        /// </summary>
        /// <param name="id" example="123">Resident id</param>
        /// <param name="helpRequestId"></param>
        /// <param name="request"></param>
        /// <response code="200">Help request is updated</response>
        /// <response code="404">...</response>
        [ProducesResponseType(typeof(ResidentResponseBoundary), StatusCodes.Status200OK)]
        [HttpPatch]
        [Route("{help-request-id}")]
        public IActionResult PatchResidentHelpRequest([FromRoute(Name = "id")] int id, [FromRoute(Name = "help-request-id")] int helpRequestId, [FromBody] ResidentHelpRequestRequest request)
        {
            var response = _patchResidentHelpRequestUseCase.Execute(id, helpRequestId, request);
            if (response != null)
                return Ok(response);
            return (NotFound("Resident not found"));
        }
    }
}
