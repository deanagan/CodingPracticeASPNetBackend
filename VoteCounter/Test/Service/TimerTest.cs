using Xunit;
using Xunit.Abstractions;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

using Api.Services;
using Api.Interfaces;

namespace Test.Controller
{
    public class TimerTest
    {
        private readonly ILogger<TimedBaseRateLimiter> _logger;
        private ITimer timer;
        public TimerTest(ITestOutputHelper testOutputHelper)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<TimedBaseRateLimiter>();
            timer = Mock.Of<ITimer>();
        }

        [Fact]
        public void Timer_Started()
        {

            var timer = new RateTimer(_logger);

           // timer.StartTimer(1, (id) => _logger.LogTrace($"Callback done for {id}"));
            // Arrange
            // var sut = new TimedBaseRateLimiter(_logger, timer);
            // var customerId = 1;

            // // Act
            // var result = true;
            // foreach(var i in Enumerable.Range(1, 999))
            // {
            //     result = result & sut.RateLimit(customerId);
            // }

            // // Assert
            // result.Should().BeTrue();
        }


    }
}
