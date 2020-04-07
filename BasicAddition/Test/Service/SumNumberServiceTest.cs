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
        [InlineData(new object[] {new int[]{2, 7, 11, 15}, 35})]
        [InlineData(new object[] {new int[]{3, 3}, 6 })]
        [InlineData(new object[] {new int[]{3, 2, 4}, 9})]
        [InlineData(new object[] {new int[]{3, 2, 3}, 8})]
        [InlineData(new object[] {new int[]{0, 4, 3, 0}, 7})]
        [InlineData(new object[] {new int[]{-3, 4, 3, 90}, 94})]
        public void CorrectSum_WhenAddingAllNumbers(int[] numbers, int expected)
        {
            // Arrange
            var sumNumberService = new SumNumberService(_logger);

            // Act
            var result = sumNumberService.AddNumbers(numbers);

            // Assert
            result.Should().Be(expected);
        }



    }
}
