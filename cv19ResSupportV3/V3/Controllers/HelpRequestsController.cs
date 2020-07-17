using System;
using cv19ResRupportV3.V3.Boundary.Response;
using cv19ResRupportV3.V3.Domain;
using cv19ResRupportV3.V3.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cv19ResRupportV3.V3.Controllers
{
    [ApiController]
    //TODO: Rename to match the APIs endpoint
    [Route("api/v3/help-requests")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    //TODO: rename class to match the API name
    public class cv19ResRupportV3Controller : BaseController
    {
        private readonly ICreateHelpRequestUseCase _createHelpRequestUseCase;
        public cv19ResRupportV3Controller(ICreateHelpRequestUseCase createHelpRequestUseCase)
        {
            _createHelpRequestUseCase = createHelpRequestUseCase;
        }

        //TODO: add xml comments containing information that will be included in the auto generated swagger docs (https://github.com/LBHackney-IT/lbh-base-api/wiki/Controllers-and-Response-Objects)
        /// <summary>
        /// ...
        /// </summary>
        /// <response code="201">...</response>
        /// <response code="400">Invalid Query Parameter.</response>
        [ProducesResponseType(typeof(HelpRequestResponse), StatusCodes.Status201Created)]
        [HttpGet]
        public IActionResult CreateHelpRequest(HelpRequest request)
        {
            var result = _createHelpRequestUseCase.Execute(request);
            return Created(new Uri("api/v3/help-requests" + result.Id), result);
        }
    }
}
