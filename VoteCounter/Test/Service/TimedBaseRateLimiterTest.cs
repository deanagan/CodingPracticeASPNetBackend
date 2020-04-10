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
    public class ProductServiceTest
    {
        private readonly ILogger<TimedBaseRateLimiter> _logger;
        private ITimer timer;
        private readonly int customerId = 1;
        public ProductServiceTest(ITestOutputHelper testOutputHelper)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<TimedBaseRateLimiter>();
            timer = Mock.Of<ITimer>();
        }

        [Fact]
        public void RateLimiterReturnsTrue_WhenCheckingLessThanMax_TimerIsNotExpired()
        {
            // Arrange
            var sut = new TimedBaseRateLimiter(timer, maxRequest: 1000, _logger);
            Mock.Get(timer).Setup(t => t.IsTimerStarted(customerId)).Returns(true);

            // Act
            var result = Enumerable.Range(1, 999).All(_ => sut.RateLimit(customerId));

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void RateLimiterReturnsFalse_WhenOverMax_TimerStartedButNotExpired()
        {
            // Arrange
            var sut = new TimedBaseRateLimiter(timer, maxRequest: 1000, _logger);
            Mock.Get(timer).Setup(t => t.IsTimerStarted(customerId)).Returns(true);

            // Act
            var result = Enumerable.Range(1, 1001).All(_ => sut.RateLimit(customerId));

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void RateLimiterReturnsTrue_WhenTimerExpiredBeforeMaxCallsExceeded()
        {
            // Arrange
            var maxRequest = 10;
            var sut = new TimedBaseRateLimiter(timer, maxRequest, _logger);
            CallBack callBack = null;
            var numberOfCalls = 0;

            // Return false, then true on next invocation
            Mock.Get(timer).Setup(t => t.IsTimerStarted(customerId)).Returns(() =>
                {
                    var result = numberOfCalls > 0;
                    numberOfCalls++;
                    return result;
                });

            Mock.Get(timer).Setup(t => t.StartTimer(customerId, It.IsAny<int>(), It.IsAny<CallBack>()))
                                        .Callback<int, int, CallBack>((id, t, cb) => callBack = cb);

            // Act
            var result = Enumerable.Range(0, maxRequest + 5).All(n =>
            {
                if (n == (maxRequest - 2)) { callBack(customerId); numberOfCalls = 0; }
                return sut.RateLimit(customerId);
            });

            // Assert
            result.Should().BeTrue();
            Mock.Get(timer).Verify(
                t => t.StartTimer(customerId, It.IsAny<int>(), It.IsAny<CallBack>()), Times.Exactly(2));
        }


    }
}
