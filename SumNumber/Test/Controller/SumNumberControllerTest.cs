using Xunit;
using Xunit.Abstractions;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

using Api.Interfaces;
using Api.Controllers;

namespace Test.Controller
{
    public class SumNumberControllerTest
    {
        private readonly ILogger<SumNumberController> _logger;

        public SumNumberControllerTest(ITestOutputHelper testOutputHelper)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<SumNumberController>();
        }
        [Fact(Skip="Needs testing")]
        public void ProductShouldBeReturned_WhenItExists()
        {
           // Todo: Test Controller
        }

    }
}
