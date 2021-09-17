using System;
using System.Collections.Generic;
using cv19ResSupportV3.V3.Controllers;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using cv19ResSupportV3.V4.Boundary.Requests;
using cv19ResSupportV3.V4.Boundary.Responses;
using cv19ResSupportV3.V4.Factories;
using cv19ResSupportV3.V4.Helpers;
using cv19ResSupportV3.V4.UseCase.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cv19ResSupportV3.V4.Controllers
{

    [ApiController]
    [Route("api/v4/residents/{id}")]
    [Produces("application/json")]
    // Check service api version information
    [ApiVersion("3.0")]

    public class CaseNotesController : BaseController
    {
        private readonly ICreateCaseNoteUseCase _addCaseNoteUseCase;
        private readonly IGetCaseNotesByResidentIdUseCase _getCaseNotesByResidentId;
        private readonly IGetCaseNotesByHelpRequestIdUseCase _getCaseNotesByHelpRequestId;

        public CaseNotesController(ICreateCaseNoteUseCase addCaseNoteUseCase, IGetCaseNotesByResidentIdUseCase getCaseNotesByResidentId, IGetCaseNotesByHelpRequestIdUseCase getCaseNotesByHelpRequestId)
        {
            _addCaseNoteUseCase = addCaseNoteUseCase;
            _getCaseNotesByHelpRequestId = getCaseNotesByHelpRequestId;
            _getCaseNotesByResidentId = getCaseNotesByResidentId;
        }


        /// <summary>
        /// Creates a case note with the values provided.
        /// </summary>
        /// <param name="id" example="123">Resident id</param>
        /// <param name="help-request-id" example="456">Help request id</param>
        /// <response code="201">...</response>
        [ProducesResponseType(typeof(CaseNoteResponse), StatusCodes.Status201Created)]
        [HttpPost]
        [Route("help-requests/{help-request-id}/case-notes")]
        public IActionResult CreateCaseNote([FromRoute(Name = "id")] int residentId, [FromRoute(Name = "help-request-id")] int helpRequestId, [FromBody] CreateCaseNoteRequest caseNote)
        {
            var response = _addCaseNoteUseCase.Execute(residentId, helpRequestId, caseNote.CaseNote);
            return Created(new Uri($"api/v4/residents/{residentId}/help-requests/{helpRequestId}/case-notes/{response}", UriKind.Relative), response);
        }


        /// <summary>
        /// Returns case notes belonging to the resident
        /// </summary>
        /// <param name="id" example="123">Resident id</param>
        /// <response code="200">...</response>
        [ProducesResponseType(typeof(List<CaseNoteResponse>), StatusCodes.Status201Created)]
        [HttpGet]
        [Route("case-notes")]
        public IActionResult GetCaseNotesByResidentId([FromRoute(Name = "id")] int residentId, string includeType)
        {
            var response = _getCaseNotesByResidentId.Execute(residentId, DataFilteringHelpers.GetExcludedHelpTypes(includeType));
            return Ok(response.ToResponse());
        }

        /// <summary>
        /// Returns case notes belonging to the help request
        /// </summary>
        /// <param name="id" example="123">Resident id</param>
        /// <param name="help-request-id" example="456">Help request id</param>
        /// <response code="200">...</response>
        [ProducesResponseType(typeof(List<CaseNoteResponse>), StatusCodes.Status201Created)]
        [HttpGet]
        [Route("help-requests/{help-request-id}/case-notes")]
        public IActionResult GetCaseNotesByHelpRequestId(
            [FromRoute(Name = "id")] int residentId,
            [FromRoute(Name = "help-request-id")] int helpRequestId,
            string includeType)
        {
            var response = _getCaseNotesByHelpRequestId.Execute(helpRequestId, DataFilteringHelpers.GetExcludedHelpTypes(includeType));
            return Ok(response.ToResponse());
        }
    }
}
