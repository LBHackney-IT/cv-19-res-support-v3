using System;
using System.Collections.Generic;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Factories.Commands;
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
        private readonly IUpdateResidentAndHelpRequestUseCase _updateResidentAndHelpRequestUseCase;
        private readonly IPatchResidentAndHelpRequestUseCase _patchResidentAndHelpRequestUseCase;
        private readonly IGetResidentsAndHelpRequestsUseCase _getResidentsAndHelpRequestsUseCase;
        private readonly IGetResidentAndHelpRequestUseCase _getResidentAndHelpRequestUseCase;
        private readonly ICreateResidentAndHelpRequestUseCase _createResidentAndHelpRequestUse;
        private readonly IUpdateStaffAssignmentsUseCase _updateStaffAssignmentsUseCase;
        public HelpRequestsController(IGetResidentsAndHelpRequestsUseCase getResidentsAndHelpRequestsUseCase,
            IUpdateResidentAndHelpRequestUseCase updateResidentAndHelpRequestUseCase, IGetResidentAndHelpRequestUseCase getResidentAndHelpRequestUseCase, IPatchResidentAndHelpRequestUseCase patchResidentAndHelpRequestUseCase, ICreateResidentAndHelpRequestUseCase createResidentAndHelpRequestUseCase, IUpdateStaffAssignmentsUseCase updateStaffAssignmentsUseCase)
        {
            _updateResidentAndHelpRequestUseCase = updateResidentAndHelpRequestUseCase;
            _patchResidentAndHelpRequestUseCase = patchResidentAndHelpRequestUseCase;
            _getResidentsAndHelpRequestsUseCase = getResidentsAndHelpRequestsUseCase;
            _getResidentAndHelpRequestUseCase = getResidentAndHelpRequestUseCase;
            _createResidentAndHelpRequestUse = createResidentAndHelpRequestUseCase;
            _updateStaffAssignmentsUseCase = updateStaffAssignmentsUseCase;
        }

        /// <summary>
        /// Creates a help request with the values provided.
        /// </summary>
        /// <response code="201">...</response>
        [ProducesResponseType(typeof(HelpRequestCreateResponse), StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult CreateResidentAndHelpRequest(HelpRequestCreateRequestBoundary request)
        {
            try
            {
                var command = request.ToCommand();
                var id = _createResidentAndHelpRequestUse.Execute(command);
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
        [ProducesResponseType(typeof(HelpRequestResponse), StatusCodes.Status200OK)]
        [HttpPut]
        public IActionResult UpdateResidentAndHelpRequest(HelpRequestUpdateRequest request)
        {
            try
            {
                var command = request.ToCommand();
                var response = _updateResidentAndHelpRequestUseCase.Execute(command);
                var result = response.ToResponse();
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
        public IActionResult PatchResidentAndHelpRequest([FromRoute] int id, [FromBody] HelpRequestPatchRequest request)
        {
            try
            {
                var command = request.ToCommand();
                _patchResidentAndHelpRequestUseCase.Execute(id, command);
                var response = new Dictionary<string, string>();
                response.Add("success", "true");
                return Ok(response);
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
        [ProducesResponseType(typeof(List<HelpRequestResponse>), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetResidentsAndHelpRequests([FromQuery] RequestQueryParams requestParams)
        {
            Console.WriteLine(JsonConvert.SerializeObject(requestParams));
            var command = requestParams.ToCommand();
            var result = _getResidentsAndHelpRequestsUseCase.Execute(command);
            return Ok(result.ToResponse());
        }

        /// <summary>
        /// Returns a single help request matching the provided id.
        /// </summary>
        /// <response code="200">Record retrieved successfully</response>
        /// <response code="404">A record with the specified id was not found</response>
        [ProducesResponseType(typeof(HelpRequestResponse), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetResidentAndHelpRequest(int id)
        {
            var result = _getResidentAndHelpRequestUseCase.Execute(id);
            if (result == null)
                return NotFound();
            return Ok(result.ToResponse());
        }

        [HttpPost]
        [Route("staff_assignments")]
        public IActionResult UpdateStaffAssignments(UpdateStaffAssignmentsRequestBoundary request)
        {
            _updateStaffAssignmentsUseCase.Execute(request);
            return Ok();
        }
    }
}
