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
    public class SumNumberController : ControllerBase
    {
        private readonly ISumNumberService productService;
        private readonly ILogger _logger;

        public SumNumberController(ISumNumberService productService, ILogger<SumNumberController> logger)
        {
            this.productService = productService;
            this._logger = logger;
        }

        [HttpGet("[action]")]
        public IActionResult SumNumber(List<int> numbers, int target)
        {
            return Ok();
        }

    }
}
