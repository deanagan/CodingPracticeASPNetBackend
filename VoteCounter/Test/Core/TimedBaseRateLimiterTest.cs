using Xunit;
using Xunit.Abstractions;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using System.Linq;

using Api.Services;
using Api.Interfaces;
using Api.Controllers;

namespace Test.Controller
{
    public class TimedBaseRateLimiterTest
    {
        private readonly ILogger<TimedBaseRateLimiter> _logger;
        private ITimer _timer;
        private readonly string CANDIDATE = "Foo";
        private readonly int MAX_REQUESTS = 10;
        private IOptions<VoteCounterControllerSettings> _options;
        private TimedBaseRateLimiter _rateLimiter;
        public TimedBaseRateLimiterTest(ITestOutputHelper testOutputHelper)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<TimedBaseRateLimiter>();
            _timer = Mock.Of<ITimer>();
            _options = Mock.Of<IOptions<VoteCounterControllerSettings>>(options => options.Value.MaxRequests == MAX_REQUESTS);
            _rateLimiter = new TimedBaseRateLimiter(_timer, _options, _logger);
        }

        private void ResetMockTimer()
        {
            var timerStarted = false;

            Mock.Get(_timer).Setup(t => t.IsTimerStarted(CANDIDATE)).Returns(() =>
                {
                    if (timerStarted == false)
                    {
                        timerStarted = true;
                        return false;
                    }
                    return true;
                });
        }

        [Fact]
        public void RateLimiterReturnsTrue_WhenCheckingLessThanMax_TimerIsNotExpired()
        {
            // Arrange
            Mock.Get(_timer).Setup(t => t.IsTimerStarted(CANDIDATE)).Returns(true);

            // Act
            var result = Enumerable.Range(1, 9).All(_ => _rateLimiter.RateLimit(CANDIDATE));

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void RateLimiterReturnsFalse_WhenOverMax_TimerStartedButNotExpired()
        {
            // Arrange
            Mock.Get(_timer).Setup(t => t.IsTimerStarted(CANDIDATE)).Returns(true);

            // Act
            var result = Enumerable.Range(1, 11).All(_ => _rateLimiter.RateLimit(CANDIDATE));

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void RateLimiterReturnsTrue_WhenTimerExpiredBeforeMaxCallsExceeded()
        {
            // Arrange
            var timerExpirySimulationMethods = new List<CallBack>{(_)=>ResetMockTimer()};

            Mock.Get(_timer).Setup(t => t.StartTimer(CANDIDATE, It.IsAny<int>(), It.IsAny<CallBack>()))
                                        .Callback<string, int, CallBack>((_, timeMs, cb) => timerExpirySimulationMethods.Add(cb));

            // Act
            var result = Enumerable.Range(0, MAX_REQUESTS - 2).All(n => _rateLimiter.RateLimit(CANDIDATE));
            timerExpirySimulationMethods.ForEach((cb) => cb(CANDIDATE));
            result &= Enumerable.Range(0, MAX_REQUESTS - 2).All(n => _rateLimiter.RateLimit(CANDIDATE));

            // Assert
            result.Should().BeTrue();
        }

    }
}
