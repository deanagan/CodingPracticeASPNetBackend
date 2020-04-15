using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

using Api.Interfaces;
using Api.Filters;

namespace Api.Controllers
{
    public class VoteCounterControllerSettings
    {
        public int MaxRequests { get; set; }

        // ...
    }

    [ApiController]
    [Route("api/[controller]")]
    public class VoteCounterController : ControllerBase
    {

        private readonly ILogger _logger;
        private IVoteService _voteService;
        private readonly VoteCounterControllerSettings _settings;
        public IRateLimiter RateLimiter { get; }
        public VoteCounterController(IOptions<VoteCounterControllerSettings> options, IRateLimiter rateLimiter, IVoteService voteService, ILogger<VoteCounterController> logger)
        {
            this._voteService = voteService;
            this._logger = logger;
            this.RateLimiter = rateLimiter;
            this._settings = options.Value;
        }

        [HttpGet("[action]")]
        public IActionResult GetWinner()
        {
            // Return list of vote instances with top vote count

            return Ok();
        }

        [HttpPost("[action]/{id}")]
        [TimeBasedAPILimiterFilter]
        public IActionResult AddVote(int id)
        {
            //_voteService.AddVoteFor(id);
            return Ok();
        }

        [HttpPut("[action]")]
        public IActionResult AddVote(int id, int votesToAdd)
        {
            _voteService.AddVoteFor(id, votesToAdd);
            return Ok();
        }

    }
}
