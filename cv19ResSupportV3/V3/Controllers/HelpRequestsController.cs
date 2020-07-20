using System;
using System.Diagnostics;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cv19ResSupportV3.V3.Controllers
{
    [ApiController]
    //TODO: Rename to match the APIs endpoint
    [Route("api/v3/help-requests")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    //TODO: rename class to match the API name
    public class HelpRequestsController : BaseController
    {
        private readonly ICreateHelpRequestUseCase _createHelpRequestUseCase;
        public HelpRequestsController(ICreateHelpRequestUseCase createHelpRequestUseCase)
        {
            _createHelpRequestUseCase = createHelpRequestUseCase;
        }

        //TODO: add xml comments containing information that will be included in the auto generated swagger docs (https://github.com/LBHackney-IT/lbh-base-api/wiki/Controllers-and-Response-Objects)
        /// <summary>
        /// ...
        /// </summary>
        /// <response code="201">...</response>
        [ProducesResponseType(typeof(HelpRequestResponse), StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult CreateHelpRequest(HelpRequest request)
        {
            Console.WriteLine("Testing");
            var result = _createHelpRequestUseCase.Execute(request);
            Console.WriteLine(result.Id.ToString());
            return Created(new Uri($"api/v3/help-requests/{result.Id}", UriKind.Relative), result);
        }
    }
}
