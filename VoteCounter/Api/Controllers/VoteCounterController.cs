using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

using Api.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoteCounterController : ControllerBase
    {

        private readonly ILogger _logger;

        public VoteCounterController(ILogger<VoteCounterController> logger)
        {

            this._logger = logger;
        }

        [HttpGet("[action]")]
        public IActionResult TopVoters()
        {

            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult AddVote(string name)
        {
            return Ok();
        }


    }
}
