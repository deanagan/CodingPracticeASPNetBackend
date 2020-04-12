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
        private IVoteService _voteService;

        public VoteCounterController(IVoteService voteService, ILogger<VoteCounterController> logger)
        {
            this._voteService = voteService;
            this._logger = logger;
        }

        [HttpGet("[action]")]
        public IActionResult GetWinner()
        {
            // Return list of vote instances with top vote count
            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult AddVote(int id)
        {
            _voteService.AddVoteFor(id);
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
