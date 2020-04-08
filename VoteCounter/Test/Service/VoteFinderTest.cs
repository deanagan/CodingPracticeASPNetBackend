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
    public class VoteFinderTest
    {
        private readonly ILogger<TimedBaseRateLimiter> _logger;

        public VoteFinderTest(ITestOutputHelper testOutputHelper)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<TimedBaseRateLimiter>();
        }

        [Theory]
        [InlineData(new object[] {new string[]{"A", "B", "A", "C", "D", "B", "A"}, "A", true})]
        [InlineData(new object[] {new string[]{"A", "A", "B", "B"}, "A", true})]
        [InlineData(new object[] {new string[]{"A", "B", "B", "A"}, "B", true})]

        [InlineData(new object[] {new string[]{"A", "B", "A", "C", "D", "B", "A"}, "A", false})]
        [InlineData(new object[] {new string[]{"A", "A", "B", "B"}, "B", false})]
        [InlineData(new object[] {new string[]{"A", "B", "B", "A"}, "A", false})]

        // [InlineData(new object[] {new int[]{3, 3}, 6 })]
        // [InlineData(new object[] {new int[]{3, 2, 4}, 9})]
        // [InlineData(new object[] {new int[]{3, 2, 3}, 8})]
        // [InlineData(new object[] {new int[]{0, 4, 3, 0}, 7})]
        // [InlineData(new object[] {new int[]{-3, 4, 3, 90}, 94})]
        public void FindWinner_BasicExample1(string[] inputs, string expected, bool doesFirstToHitMax)
        {
            // Arrange
            var sut = new VoteFinder(_logger, doesFirstToHitMax);

            // Act
            var result = sut.FindWinner(inputs.ToList());
            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void FindWinner_PointSystem()
        {
            var listOfVote = new List<Vote> { new Vote { Name="A", Count=5}, new Vote { Name="B", Count=3} };

            var sut = new VoteFinder(_logger, true);

            var result = sut.FindWinner(listOfVote);

            result.Should().Be("A");
        }




    }
}
