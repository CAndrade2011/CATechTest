using Domain.Aggregate;
using Domain.Repository;
using Domain.Service;
using Infra.DataFromMongo.Entity;

namespace BusinessLogicLayer.Service;

public class ProductService: IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> CreateProductAsync(ProductAggregate product)
    {
        return await _productRepository.CreateProductAsync(product);
    }

    public async Task<bool> DeleteProductAsync(string id)
    {
        return await _productRepository.DeleteProductAsync(id);
    }

    public async Task<List<ProductAggregate>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllProductsAsync();
    }

    public async Task<ProductAggregate> GetProductByIdAsync(string id)
    {
        return await _productRepository.GetProductByIdAsync(id);
    }

    public async Task<bool> UpdateProductAsync(ProductAggregate product)
    {
        return await _productRepository.UpdateProductAsync(product);
    }
}
