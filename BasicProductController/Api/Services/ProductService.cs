using System.Collections.Generic;
using System.Linq;

using Api.Models;
using Api.Interfaces;

namespace Api.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository productRepository;
        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public List<string> GetAllSkuCodes()
        {
            return productRepository.GetProducts().Select(product => product.SkuCode).ToList();
        }

        public Product GetProduct(string skuCode)
        {
            return productRepository.GetProducts().Where(product => product.SkuCode == skuCode)
                    .Select(matchingProduct => matchingProduct)
                    .DefaultIfEmpty(null)
                    .First();
        }

    }
}
