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
    //TODO: Rename to match the APIs endpoint
    [Route("api/v3/help-requests")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    //TODO: rename class to match the API name
    public class HelpRequestsController : BaseController
    {
        private readonly ICreateHelpRequestUseCase _createHelpRequestUseCase;
        private readonly IUpdateHelpRequestUseCase _updateHelpRequestUseCase;
        private readonly IGetHelpRequestsUseCase _getHelpRequestsUseCase;
        private readonly IGetHelpRequestUseCase _getHelpRequestUseCase;
        public HelpRequestsController(ICreateHelpRequestUseCase createHelpRequestUseCase, IGetHelpRequestsUseCase getHelpRequestsUseCase,
            IUpdateHelpRequestUseCase updateHelpRequestUseCase, IGetHelpRequestUseCase getHelpRequestUseCase)
        {
            _createHelpRequestUseCase = createHelpRequestUseCase;
            _updateHelpRequestUseCase = updateHelpRequestUseCase;
            _getHelpRequestsUseCase = getHelpRequestsUseCase;
            _getHelpRequestUseCase = getHelpRequestUseCase;
        }

        //TODO: add xml comments containing information that will be included in the auto generated swagger docs (https://github.com/LBHackney-IT/lbh-base-api/wiki/Controllers-and-Response-Objects)
        /// <summary>
        /// ...
        /// </summary>
        /// <response code="201">...</response>
        [ProducesResponseType(typeof(HelpRequestCreateResponse), StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult CreateHelpRequest(HelpRequest request)
        {
            var result = _createHelpRequestUseCase.Execute(request);
            return Created(new Uri($"api/v3/help-requests/{result.Id}", UriKind.Relative), result);
        }

        //TODO: add xml comments containing information that will be included in the auto generated swagger docs (https://github.com/LBHackney-IT/lbh-base-api/wiki/Controllers-and-Response-Objects)
        /// <summary>
        /// ...
        /// </summary>
        /// <response code="200">...</response>
        [ProducesResponseType(typeof(HelpRequestCreateResponse), StatusCodes.Status200OK)]
        [HttpPut]
        public IActionResult UpdateHelpRequest(HelpRequest request)
        {
            var result = _updateHelpRequestUseCase.Execute(request);
            return Ok(result);
        }

        //TODO: add xml comments containing information that will be included in the auto generated swagger docs (https://github.com/LBHackney-IT/lbh-base-api/wiki/Controllers-and-Response-Objects)
        /// <summary>
        /// ...
        /// </summary>
        /// <response code="200">...</response>
        [ProducesResponseType(typeof(List<HelpRequestGetResponse>), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetHelpRequests([FromQuery] RequestQueryParams requestParams)
        {
            Console.WriteLine(JsonConvert.SerializeObject(requestParams));
            var result = _getHelpRequestsUseCase.Execute(requestParams);
            return Ok(result);
        }

        //TODO: add xml comments containing information that will be included in the auto generated swagger docs (https://github.com/LBHackney-IT/lbh-base-api/wiki/Controllers-and-Response-Objects)
        /// <summary>
        /// ...
        /// </summary>
        /// <response code="200">...</response>
        [ProducesResponseType(typeof(HelpRequest), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetHelpRequest(int id)
        {
            var result = _getHelpRequestUseCase.Execute(id);
            return Ok(result);
        }


    }
}
