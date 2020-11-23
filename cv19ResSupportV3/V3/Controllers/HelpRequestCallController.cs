using System;
using System.Collections.Generic;
using System.Diagnostics;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
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
    public class HelpRequestCallController : BaseController
    {
        private readonly ICreateHelpRequestCallUseCase _createHelpRequestCallUseCase;
        public HelpRequestCallController(ICreateHelpRequestCallUseCase createHelpRequestCallUseCase)
        {
            _createHelpRequestCallUseCase = createHelpRequestCallUseCase;
        }

        /// <summary>
        /// Creates a help request with the values provided.
        /// </summary>
        /// <response code="201">...</response>
        [ProducesResponseType(typeof(HelpRequestCreateResponse), StatusCodes.Status201Created)]
        [HttpPost]
        [Route("{id}/calls")]
        public IActionResult CreateHelpRequestCall([FromRoute] int id, [FromBody] HelpRequestCall request)
        {
            try
            {
                request.HelpRequestId = id;
                var result = _createHelpRequestCallUseCase.Execute(request);
                return Created(new Uri($"api/v3/help-requests/{id}/calls/{result.Id}", UriKind.Relative), result);
            }
            catch (Exception e)
            {
                return BadRequest($"Call record not created. {e}");
            }
        }


    }
}
