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
        private readonly ILogger<SumNumberService> _logger;
        public ProductServiceTest(ITestOutputHelper testOutputHelper)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<SumNumberService>();

        }

        [Theory]
        [InlineData(new object[] {new int[]{2, 7, 11, 15}, 9, new int[] {0,1}})]
        [InlineData(new object[] {new int[]{3, 3}, 6, new int[] {0,1}})]
        [InlineData(new object[] {new int[]{3, 2, 4}, 6, new int[] {1,2}})]
        [InlineData(new object[] {new int[]{3, 2, 3}, 6, new int[] {0,2}})]
        public void ReturnCorrectPair_WhenFindingSuppliedTarget(int[] numbers, int target, int[] expected)
        {
            // Arrange
            var sumNumberService = new SumNumberService(_logger);

            // Act
            var result = sumNumberService.GetElementsThatHitTarget(numbers, target);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }



    }
}
