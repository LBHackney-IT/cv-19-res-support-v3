using System;
using cv19ResSupportV3.V3.Controllers;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using cv19ResSupportV3.V4.Boundary.Requests;
using cv19ResSupportV3.V4.Boundary.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cv19ResSupportV3.V4.Controllers
{

    [ApiController]
    [Route("api/v4/residents/{id}/help-requests/{help-request-id}/case-notes")]
    [Produces("application/json")]
    // Check service api version information
    [ApiVersion("3.0")]

    public class CaseNotesController : BaseController
    {
        private readonly ICreateCaseNoteUseCase _addCaseNoteUseCase;

        public CaseNotesController(ICreateCaseNoteUseCase addCaseNoteUseCase)
        {
            _addCaseNoteUseCase = addCaseNoteUseCase;
        }


        /// <summary>
        /// Creates a resident help request with the values provided.
        /// </summary>
        /// <response code="201">...</response>
        [ProducesResponseType(typeof(CaseNoteResponse), StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult CreateCaseNote([FromRoute(Name = "id")] int residentId, [FromRoute(Name = "help-request-id")] int helpRequestId, [FromBody] CreateCaseNoteRequest caseNote)
        {
            var response = _addCaseNoteUseCase.Execute(residentId, helpRequestId, caseNote.CaseNote);
            return Created(new Uri($"api/v4/residents/{residentId}/help-requests/{helpRequestId}/case-notes/{response}", UriKind.Relative), response);
        }
    }
}
