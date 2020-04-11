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
using Api.Models;

namespace Test.Controller
{
    public class VoteCounterTest
    {
        private readonly ILogger<TimedBaseRateLimiter> _logger;

        public VoteCounterTest(ITestOutputHelper testOutputHelper)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<TimedBaseRateLimiter>();
        }

        [Theory]
        [InlineData(new object[] {new string[]{"A", "B", "A", "C", "D", "B", "A"}, "A"})]
        [InlineData(new object[] {new string[]{"A", "A", "B", "B"}, "A"})]
        [InlineData(new object[] {new string[]{"A", "B", "B", "A"}, "B"})]
        public void FindWinner_BasicExample1(string[] inputs, string expected)
        {
            // Arrange
            var sut = new VoteCounter(_logger);

            // Act
            var result = sut.FindWinner(inputs.ToList());
            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void FindWinner_PointSystem()
        {
            var listOfVote = new List<Vote> { new Vote { Name="A", Count=5}, new Vote { Name="B", Count=3} };

            var sut = new VoteCounter(_logger);

            var result = sut.FindWinner(listOfVote);

            result.Should().Be("A");
        }
    }
}
