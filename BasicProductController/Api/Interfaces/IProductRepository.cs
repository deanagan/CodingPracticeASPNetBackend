using System.Collections.Generic;

using Api.Models;

namespace Api.Interfaces
{
    //TODO: Could be generic. Not generic for now.
    public interface IProductRepository
    {
        List<Product> GetProducts();
    }
}