using Xunit;
using Xunit.Abstractions;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

using Api.Models;
using Api.Interfaces;
using Api.Controllers;

namespace Test.Controller
{
    public class VoteCounterControllerTest
    {

        public VoteCounterControllerTest(ITestOutputHelper testOutputHelper)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<VoteCounterControllerTest>();
        }
        [Fact]
        public void VoteWinnerShouldBeReturned_WhenRequested()
        {
            // Arrange


            // Act


            // Assert

        }

    }
}
