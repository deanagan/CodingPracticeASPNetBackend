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
        public ProductServiceTest(ITestOutputHelper testOutputHelper)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<TimedBaseRateLimiter>();
            timer = Mock.Of<ITimer>();
        }

        // [Theory]
        // [InlineData(new object[] {new int[]{2, 7, 11, 15}, 35})]
        // [InlineData(new object[] {new int[]{3, 3}, 6 })]
        // [InlineData(new object[] {new int[]{3, 2, 4}, 9})]
        // [InlineData(new object[] {new int[]{3, 2, 3}, 8})]
        // [InlineData(new object[] {new int[]{0, 4, 3, 0}, 7})]
        // [InlineData(new object[] {new int[]{-3, 4, 3, 90}, 94})]
        [Fact]
        public void RateLimiterReturnsTrue_WhenCheckingLessThanMax_TimerIsNotExpired()
        {
            // Arrange
            var sut = new TimedBaseRateLimiter(_logger, timer);
            var customerId = 1;

            // Act
            var result = true;
            foreach(var i in Enumerable.Range(1, 999))
            {
                result = result & sut.RateLimit(customerId);
            }

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void RateLimiterReturnsFalse_WhenOverMax_TimerStartedButNotExpired()
        {
            // Arrange
            var sut = new TimedBaseRateLimiter(_logger, timer);
            var customerId = 1;

            Mock.Get(timer).Setup(t => t.IsTimerStarted(customerId)).Returns(true);

            // Act
            var result = true;
            foreach(var i in Enumerable.Range(1, 1001))
            {
                result = result & sut.RateLimit(customerId);
            }

            // Assert
            result.Should().BeFalse();
        }

        [Fact(Skip= "Skip for now")]
        public void RateLimiterReturnsTrue_WhenTimerExpiredAfterMaxCalls()
        {
            // Arrange
            var sut = new TimedBaseRateLimiter(_logger, timer);
            var customerId = 1;

            Mock.Get(timer).Setup(t => t.IsTimerStarted(customerId)).Returns(true);

            // Act
            var result = true;
            foreach(var i in Enumerable.Range(1, 1001))
            {
                result = result & sut.RateLimit(customerId);
                if (i == 1)
                {
                    Mock.Get(timer).Setup(t => t.IsTimerStarted(customerId)).Returns(false);
                }

                if (i == 995)
                {
                    // Call timer expiry callback
                }
            }

            // Assert
            result.Should().BeFalse();
        }


    }
}
