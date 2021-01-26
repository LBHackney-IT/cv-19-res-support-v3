using System;
using System.Collections.Generic;
using cv19ResSupportV3.V3.Controllers;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V4.Boundary.Requests;
using cv19ResSupportV3.V4.UseCase.Interface;
using cv19ResSupportV3.V4.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cv19ResSupportV3.V4.Controllers
{
    [ApiController]
    [Route("api/v4/residents")]
    [Produces("application/json")]
    // Check service api version information
    [ApiVersion("4.0")]

    public class ResidentsController : BaseController
    {
        private readonly ICreateResidentsUseCase _createResidentsUseCase;
        private readonly IGetResidentsUseCase _getResidentsUseCase;
        private readonly IPatchResidentUseCase _patchResidentsUseCase;
        private readonly ISearchResidentsUseCase _searchResidentsUseCase;

        public ResidentsController(
            ICreateResidentsUseCase createResidentsUseCase,
            IGetResidentsUseCase getResidentsUseCase,
            IPatchResidentUseCase patchResidentsUseCase,
            ISearchResidentsUseCase searchResidentsUseCase)
        {
            _createResidentsUseCase = createResidentsUseCase;
            _getResidentsUseCase = getResidentsUseCase;
            _patchResidentsUseCase = patchResidentsUseCase;
            _searchResidentsUseCase = searchResidentsUseCase;
        }


        /// <summary>
        /// Creates a resident with the values provided.
        /// </summary>
        /// <response code="201">...</response>
        [ProducesResponseType(typeof(ResidentResponseBoundary), StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult CreateResident(ResidentRequestBoundary request)
        {
            var response = _createResidentsUseCase.Execute(request);
            if (response != null)
                return Created(new Uri($"api/v4/residents/{response.Id}", UriKind.Relative), response);
            return (BadRequest("Resident not created"));
        }

        /// <summary>
        /// Gets a resident with the id specified.
        /// </summary>
        /// <response code="200">...</response>
        /// <response code="404">...</response>
        [ProducesResponseType(typeof(ResidentResponseBoundary), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetResident(int id)
        {
            var response = _getResidentsUseCase.Execute(id);
            if (response != null)
                return Ok(response);
            return (NotFound("Resident not found"));
        }

        /// <summary>
        /// Gets a resident with the id specified.
        /// </summary>
        /// <response code="200">...</response>
        /// <response code="404">...</response>
        [ProducesResponseType(typeof(List<ResidentResponseBoundary>), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult SearchResidents([FromQuery] FindResident requestParams)
        {
            var response = _searchResidentsUseCase.Execute(requestParams);
            return Ok(response);
        }

        /// <summary>
        /// Updates a resident with the id specified.
        /// </summary>
        /// <response code="200">...</response>
        /// <response code="404">...</response>
        [ProducesResponseType(typeof(ResidentResponseBoundary), StatusCodes.Status200OK)]
        [HttpPatch]
        [Route("{id}")]
        public IActionResult PatchResident([FromRoute] int id, [FromBody] ResidentRequestBoundary request)
        {
            var response = _patchResidentsUseCase.Execute(id, request);
            if (response != null)
                return Ok(response);
            return (NotFound("Resident not found"));
        }
    }
}
