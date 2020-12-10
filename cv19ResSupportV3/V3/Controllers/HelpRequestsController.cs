using System;
using System.Collections.Generic;
using System.Diagnostics;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.UseCase;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace cv19ResSupportV3.V3.Controllers
{
    [ApiController]
    [Route("api/v3/help-requests")]
    [Produces("application/json")]
    [ApiVersion("3.0")]
    public class HelpRequestsController : BaseController
    {
        private readonly ICreateHelpRequestUseCase _createHelpRequestUseCase;
        private readonly IUpdateHelpRequestUseCase _updateHelpRequestUseCase;
        private readonly IPatchHelpRequestUseCase _patchHelpRequestUseCase;
        private readonly IGetHelpRequestsUseCase _getHelpRequestsUseCase;
        private readonly IGetHelpRequestUseCase _getHelpRequestUseCase;
        public HelpRequestsController(ICreateHelpRequestUseCase createHelpRequestUseCase, IGetHelpRequestsUseCase getHelpRequestsUseCase,
            IUpdateHelpRequestUseCase updateHelpRequestUseCase, IGetHelpRequestUseCase getHelpRequestUseCase, IPatchHelpRequestUseCase patchHelpRequestUseCase)
        {
            _createHelpRequestUseCase = createHelpRequestUseCase;
            _updateHelpRequestUseCase = updateHelpRequestUseCase;
            _patchHelpRequestUseCase = patchHelpRequestUseCase;
            _getHelpRequestsUseCase = getHelpRequestsUseCase;
            _getHelpRequestUseCase = getHelpRequestUseCase;
        }

        /// <summary>
        /// Creates a help request with the values provided.
        /// </summary>
        /// <response code="201">...</response>
        [ProducesResponseType(typeof(HelpRequestCreateResponse), StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult CreateHelpRequest(HelpRequestCreateRequestBoundary request)
        {
            try
            {
                var domain = request.ToDomain();
                var id = _createHelpRequestUseCase.Execute(domain);
                var result = new HelpRequestCreateResponse() { Id = id };
                return Created(new Uri($"api/v3/help-requests/{id}", UriKind.Relative), result);
            }
            catch (Exception e)
            {
                return BadRequest($"Record not created. {e}");
            }
        }

        /// <summary>
        /// Replaces an existing help request record with
        /// </summary>
        /// <response code="200">The record has been updated</response>
        /// <response code="400">There was an issue updating the record.</response>
        [ProducesResponseType(typeof(HelpRequestCreateResponse), StatusCodes.Status200OK)]
        [HttpPut]
        public IActionResult UpdateHelpRequest(HelpRequest request)
        {
            try
            {
                var result = _updateHelpRequestUseCase.Execute(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Record not updated. {e}");
            }
        }

        /// <summary>
        /// Updates an existing record with the values specified.  This only updates specified editable fields:
        /// GettingInTouchReason
        /// HelpWithAccessingFood
        /// HelpWithAccessingMedicine
        /// HelpWithAccessingOtherEssentials
        /// HelpWithDebtAndMoney
        /// HelpWithHealth
        /// HelpWithMentalHealth
        /// HelpWithAccessingInternet
        /// HelpWithSomethingElse
        /// CurrentSupport
        /// CurrentSupportFeedback
        /// FirstName
        /// LastName
        /// DobMonth
        /// DobYear
        /// DobDay
        /// ContactTelephoneNumber
        /// ContactMobileNumber
        /// EmailAddress
        /// GpSurgeryDetails
        /// NumberOfChildrenUnder18
        /// ConsentToShare
        /// CaseNotes
        /// AdviceNotes
        /// RecordStatus
        /// </summary>
        /// <response code="200">The record has been updated</response>
        /// <response code="400">There was an issue updating the record.</response>
        [HttpPatch]
        [Route("{id}")]
        public IActionResult PatchHelpRequest([FromRoute] int id, [FromBody] HelpRequest request)
        {
            try
            {
                _patchHelpRequestUseCase.Execute(id, request);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Record not updated. {e}");
            }

        }

        /// <summary>
        /// Returns a list of help requests matching the provided search parameters
        /// </summary>
        /// <response code="200">A list of 0 or more help requests was returned.</response>
        [ProducesResponseType(typeof(List<HelpRequestGetResponse>), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetHelpRequests([FromQuery] RequestQueryParams requestParams)
        {
            Console.WriteLine(JsonConvert.SerializeObject(requestParams));
            var result = _getHelpRequestsUseCase.Execute(requestParams);
            return Ok(result);
        }

        /// <summary>
        /// Returns a single help request matching the provided id.
        /// </summary>
        /// <response code="200">Record retrieved successfully</response>
        /// <response code="404">A record with the specified id was not found</response>
        [ProducesResponseType(typeof(HelpRequest), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetHelpRequest(int id)
        {
            var result = _getHelpRequestUseCase.Execute(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
