using System;
using System.Collections.Generic;
using cv19ResSupportV3.V3.Controllers;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V4.UseCase.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using cv19ResSupportV3.V4.UseCase.Interfaces;
using ICreateResidentUseCase = cv19ResSupportV3.V3.UseCase.Interfaces.ICreateResidentUseCase;

namespace cv19ResSupportV3.V4.Controllers
{
    [ApiController]
    [Route("api/v4/residents")]
    [Produces("application/json")]
    // Check service api version information
    [ApiVersion("4.0")]

    public class ResidentsController : BaseController
    {
        private readonly ICreateResidentUseCase _createResidentUseCase;
        private readonly IGetResidentsUseCase _getResidentsUseCase;
        private readonly IPatchResidentUseCase _patchResidentsUseCase;
        private readonly ISearchResidentsUseCase _searchResidentsUseCase;

        public ResidentsController(
            ICreateResidentUseCase createResidentUseCase,
            IGetResidentsUseCase getResidentsUseCase,
            IPatchResidentUseCase patchResidentsUseCase,
            ISearchResidentsUseCase searchResidentsUseCase)
        {
            _createResidentUseCase = createResidentUseCase;
            _getResidentsUseCase = getResidentsUseCase;
            _patchResidentsUseCase = patchResidentsUseCase;
            _searchResidentsUseCase = searchResidentsUseCase;
        }


        /// <summary>
        /// Creates a resident with the values provided.
        /// </summary>
        /// <response code="201">Resident is created</response>
        /// <response code="400">...</response>
        [ProducesResponseType(typeof(ResidentResponseBoundary), StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult CreateResident(CreateResident request)
        {
            var response = _createResidentUseCase.Execute(request);
            if (response != null)
                return Created(new Uri($"api/v4/residents/{response.Id}", UriKind.Relative), response);
            return (BadRequest("Resident not created"));
        }

        /// <summary>
        /// Gets a resident with the id specified.
        /// </summary>
        /// <param name="id" example="123">Resident id</param>
        /// <response code="200">Resident is returned</response>
        /// <response code="404">...</response>
        /// <response code="400">...</response>
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
        /// Gets residents matching the specified parameters.
        /// </summary>
        /// <response code="200">List of residents is returned</response>
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
        /// <param name="id" example="123">Resident id</param>
        /// <response code="200">Resident has been updated</response>
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
