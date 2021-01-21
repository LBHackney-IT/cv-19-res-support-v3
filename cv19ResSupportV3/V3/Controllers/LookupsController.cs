using System.Collections.Generic;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain.Queries;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.UseCase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cv19ResSupportV3.V3.Controllers
{
    [ApiController]
    [Route("api/v3/lookups")]
    [Produces("application/json")]
    [ApiVersion("3.0")]
    public class LookupsController : BaseController
    {
        private readonly IGetLookupsUseCase _getLookupsUseCase;
        public LookupsController(IGetLookupsUseCase getLookupsUseCase)
        {
            _getLookupsUseCase = getLookupsUseCase;
        }

        /// <summary>
        /// Returns a list of help requests requiring a callback.
        /// </summary>
        /// <response code="200">A list of 0 or more callbacks returned.</response>
        [ProducesResponseType(typeof(List<LookupResponse>), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetLookups([FromQuery] LookupQueryParams requestParams)
        {
            var query = new LookupQuery()
            {
                LookupGroup = requestParams.LookupGroup
            };
            var result = _getLookupsUseCase.Execute(query).ToResponse();
            return Ok(result);
        }

    }
}
