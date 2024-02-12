using Domain.Aggregate;

namespace Domain.Repository;

public interface IProductRepository
{
    Task<bool> CreateProductAsync(ProductAggregate product);
    Task<List<ProductAggregate>> GetAllProductsAsync();
    Task<ProductAggregate> GetProductByIdAsync(Guid id);
    Task<bool> UpdateProductAsync(ProductAggregate product);
    Task<bool> DeleteProductAsync(Guid id);

}
