using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
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

        public int MaxTimeInMilliSeconds { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class VoteCounterController : ControllerBase
    {

        private readonly ILogger _logger;
        private IVoteService _voteService;
        private readonly VoteCounterControllerSettings _settings;
        public IRateLimiter RateLimiter { get; }
        private IMemoryCache _memoryCache;
        public VoteCounterController(IOptions<VoteCounterControllerSettings> options,
                                     IRateLimiter rateLimiter,
                                     IVoteService voteService,
                                     IMemoryCache memoryCache,
                                     ILogger<VoteCounterController> logger)
        {
            this._voteService = voteService;
            this._logger = logger;
            this.RateLimiter = rateLimiter;
            this._settings = options.Value;
            this._memoryCache = memoryCache;
        }

        [HttpGet("[action]")]
        public IActionResult GetWinner()
        {
            var currentWinner = string.Empty;
            var totalVotesProcessed = _voteService.TotalVotesProcessed();
            if (!_memoryCache.TryGetValue(totalVotesProcessed, out currentWinner))
            {
                currentWinner = _voteService.GetWinner();
                _memoryCache.Set(totalVotesProcessed, currentWinner);
            }

            return Ok(currentWinner);

        }

        [HttpPost("[action]/{candidate}")]
        [TimeBasedAPILimiterFilter]
        public IActionResult AddVote(string candidate)
        {
            _voteService.AddVoteFor(candidate);
            return Ok();
        }

    }
}
