using Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service;

public interface IProductService
{
    Task<List<ProductAggregate>> GetAllProductsAsync();
    Task<ProductAggregate> GetProductByIdAsync(string id);

    Task<bool> CreateProductAsync(ProductAggregate product);
    Task<bool> UpdateProductAsync(ProductAggregate product);
    Task<bool> DeleteProductAsync(string id);
}
