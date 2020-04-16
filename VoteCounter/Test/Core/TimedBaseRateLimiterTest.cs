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
        private readonly int customerId = 1;
        private IOptions<VoteCounterControllerSettings> _options;
        private TimedBaseRateLimiter _rateLimiter;
        public TimedBaseRateLimiterTest(ITestOutputHelper testOutputHelper)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<TimedBaseRateLimiter>();
            _timer = Mock.Of<ITimer>();
            _options = Mock.Of<IOptions<VoteCounterControllerSettings>>(options => options.Value.MaxRequests == 10);
            _rateLimiter = new TimedBaseRateLimiter(_timer, _options, _logger);
        }

        [Fact]
        public void RateLimiterReturnsTrue_WhenCheckingLessThanMax_TimerIsNotExpired()
        {
            // Arrange
            Mock.Get(_timer).Setup(t => t.IsTimerStarted(customerId)).Returns(true);

            // Act
            var result = Enumerable.Range(1, 9).All(_ => _rateLimiter.RateLimit(customerId));

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void RateLimiterReturnsFalse_WhenOverMax_TimerStartedButNotExpired()
        {
            // Arrange
            Mock.Get(_timer).Setup(t => t.IsTimerStarted(customerId)).Returns(true);

            // Act
            var result = Enumerable.Range(1, 11).All(_ => _rateLimiter.RateLimit(customerId));

            // Assert
            result.Should().BeFalse();
        }

        // [Fact]
        // public void RateLimiterReturnsTrue_WhenTimerExpiredBeforeMaxCallsExceeded()
        // {
        //     // Arrange
        //     var maxRequest = 10;
        //     var sut = new TimedBaseRateLimiter(timer, configuration, _logger);
        //     CallBack callBack = null;
        //     var numberOfCalls = 0;

        //     // Return false, then true on next invocation
        //     Mock.Get(timer).Setup(t => t.IsTimerStarted(customerId)).Returns(() =>
        //         {
        //             var result = numberOfCalls > 0;
        //             numberOfCalls++;
        //             return result;
        //         });

        //     Mock.Get(timer).Setup(t => t.StartTimer(customerId, It.IsAny<int>(), It.IsAny<CallBack>()))
        //                                 .Callback<int, int, CallBack>((id, t, cb) => callBack = cb);

        //     // Act
        //     var result = Enumerable.Range(0, maxRequest + 5).All(n =>
        //     {
        //         if (n == (maxRequest - 2)) { callBack(customerId); numberOfCalls = 0; }
        //         return sut.RateLimit(customerId);
        //     });

        //     // Assert
        //     result.Should().BeTrue();
        //     Mock.Get(timer).Verify(
        //         t => t.StartTimer(customerId, It.IsAny<int>(), It.IsAny<CallBack>()), Times.Exactly(2));
        // }


    }
}
