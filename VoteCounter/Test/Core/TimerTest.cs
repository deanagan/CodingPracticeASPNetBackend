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
using System.Threading;

using Api.Services;
using Api.Interfaces;

namespace Test.Controller
{
    public class TimerTest
    {
        private readonly ILogger<RateTimer> _logger;
        private ITimer timer;
        public TimerTest(ITestOutputHelper testOutputHelper)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<RateTimer>();
            timer = Mock.Of<ITimer>();
        }

        [Fact]
        public void IdPassedToCallback_WhenTimerExpires()
        {
            var expectedId = 23;
            var returnedId = 0;
            var autoResetEvent = new AutoResetEvent(false);

            CallBack callback = (id) => {
                returnedId = id;
                autoResetEvent.Set();
            };

            var timer = new RateTimer(_logger);
            timer.StartTimer(expectedId, 500, callback);


            autoResetEvent.WaitOne().Should().BeTrue();
            returnedId.Should().Be(expectedId);

        }

        [Fact]
        public void TimerNotStarted_WhenTimerExpires()
        {
            var expectedId = 23;
            var returnedId = 0;
            var autoResetEvent = new AutoResetEvent(false);

            CallBack callback = (id) => {
                returnedId = id;
                autoResetEvent.Set();
            };

            var timer = new RateTimer(_logger);
            timer.StartTimer(expectedId, 500, callback);


            autoResetEvent.WaitOne().Should().BeTrue();
            timer.IsTimerStarted(expectedId).Should().BeFalse();

        }

    }
}
