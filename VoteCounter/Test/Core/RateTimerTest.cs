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
        public void KeyPassedToCallback_WhenTimerExpires()
        {
            // Arrange
            var expectedKey = "Foo";
            var returnedKey = string.Empty;
            var autoResetEvent = new AutoResetEvent(false);

            CallBack callback = (key) => {
                returnedKey = key;
                autoResetEvent.Set();
            };
            var timer = new RateTimer(_logger);

            // Act
            timer.StartTimer(expectedKey, 500, callback);

            // Assert
            autoResetEvent.WaitOne().Should().BeTrue();
            returnedKey.Should().Be(expectedKey);
        }

        [Fact]
        public void TimerNotStarted_WhenTimerExpires()
        {
            var expectedKey = "Foo";
            var returnedKey = string.Empty;
            var autoResetEvent = new AutoResetEvent(false);

            CallBack callback = (key) => {
                returnedKey = key;
                autoResetEvent.Set();
            };

            var timer = new RateTimer(_logger);
            timer.StartTimer(expectedKey, 500, callback);


            autoResetEvent.WaitOne().Should().BeTrue();
            timer.IsTimerStarted(expectedKey).Should().BeFalse();
        }

        [Fact]
        public void IsTimerStartedReturnsFalse_WhenIsTimerStartedInvokedForNewId()
        {
            // Arrange
            var timer = new RateTimer(_logger);

            // Act
            var result = timer.IsTimerStarted("foo");

            // Assert
            result.Should().BeFalse();
        }

    }
}
