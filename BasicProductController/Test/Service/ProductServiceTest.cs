using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

using Api.Models;
using Api.Services;
using Api.Controllers;
using Api.Interfaces;

namespace Test.Controller
{
    public class ProductServiceTest
    {
        private readonly List<Product> _products = new List<Product>
        {
            new Product
            {
                Id = 1, Price = 1.23M, SkuCode = "PROD_001", Name = "Cool 1"
            },
            new Product
            {
                Id = 2, Price = 4.56M, SkuCode = "PROD_002", Name = "Awesome 2"
            },
        };
        private readonly IProductService _productService;
        private readonly Mock<IProductRepository> _productRepositoryMock = new Mock<IProductRepository>();

        public ProductServiceTest()
        {
            _productRepositoryMock.Setup(
                pr => pr.GetProducts()).Returns(_products);

            _productService = new ProductService(_productRepositoryMock.Object);
        }

        [Fact]
        public void AllCodesReturned_WhenGetAllSkuCodesInvoked()
        {
            // Act
            var skuCodes = _productService.GetAllSkuCodes();

            // Assert
            skuCodes.Should().HaveCount(2).And.ContainInOrder("PROD_001", "PROD_002");
        }

        [Fact]
        public void CorrectProductReturned_WhenGetProductRetrievedThruSkuCode()
        {
            // Act
            var product = _productService.GetProduct("PROD_001");

            // Assert
            product.Should().Be(_products.First());
        }

        [Fact]
        public void NullProductReturned_WhenSkuCodeDoesNotExist()
        {
            // Act
            var product = _productService.GetProduct("PROD_003");

            // Assert
            product.Should().BeNull();
        }

    }
}
