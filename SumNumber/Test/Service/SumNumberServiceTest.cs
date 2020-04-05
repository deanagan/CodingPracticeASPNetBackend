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
        private readonly ISumNumberService _sumNumberService;
        private readonly ILogger<SumNumberService> _logger;
        public ProductServiceTest(ITestOutputHelper testOutputHelper)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new XunitLoggerProvider(testOutputHelper));
            _logger = loggerFactory.CreateLogger<SumNumberService>();
            _sumNumberService = new SumNumberService(_logger);
        }

        [Theory]
        [InlineData(new object[] {new int[]{2, 7, 11, 15}, 9})]
        public void AllCodesReturned_WhenGetAllSkuCodesInvoked(List<int> numbers, int target)
        {
            // Arrange

            // Act

            // Assert
        }



    }
}
