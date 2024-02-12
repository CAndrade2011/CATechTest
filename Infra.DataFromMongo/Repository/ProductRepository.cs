using Domain.Aggregate;
using Domain.Repository;
using Infra.DataFromMongo.Entity;
using MongoDB.Driver;

namespace Infra.DataFromMongo.Repository;

public class ProductRepository : BaseContext, IProductRepository, IDisposable
{
    private readonly IMongoCollection<Product> _productsCollection;
    private const string COLLECTION_NAME = "product";

    public ProductRepository(Microsoft.Extensions.Configuration.IConfiguration configuration) : base(configuration)
    {
        _productsCollection = base.GetCollection<Product>(COLLECTION_NAME);
    }

    public async Task<bool> CreateProductAsync(ProductAggregate product)
    {
        var entity = ToEntity(product);
        await _productsCollection.InsertOneAsync(entity);
        return SUCCESS_RETURN;
    }

    public async Task<List<ProductAggregate>> GetAllProductsAsync()
    {
        return ToAggregate(await _productsCollection.Find(Builders<Product>.Filter.Empty).ToListAsync());
    }

    public async Task<ProductAggregate> GetProductByIdAsync(string id)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, BaseContext.ToDbId(id));
        return ToAggregate(await _productsCollection.Find(filter).FirstOrDefaultAsync());
    }

    public async Task<bool> UpdateProductAsync(ProductAggregate product)
    {
        var entity = ToEntity(product);
        var filter = Builders<Product>.Filter.Eq(p => p.Id, entity.Id);
        var result = await _productsCollection.ReplaceOneAsync(filter, entity);
        return result.ModifiedCount >= 1 ? SUCCESS_RETURN : ERROR_RETURN;
    }

    public async Task<bool> DeleteProductAsync(string id)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, ToDbId(id));
        var result = await _productsCollection.DeleteOneAsync(filter);
        return result.DeletedCount >= 1 ? SUCCESS_RETURN : ERROR_RETURN;
    }

    private static Product ToEntity(ProductAggregate? obj)
    {
        if (obj == null) return new();
        return new()
        {
            Id = BaseContext.ToDbId(obj.Id),
            BarCode = obj.BarCode,
            Name = obj.Name,
            Quantity = obj.Quantity
        };
    }

    private static ProductAggregate ToAggregate(Product? obj)
    {
        if (obj == null) return new();
        return new()
        {
            Id = BaseContext.ToSystemId(obj.Id),
            BarCode = obj.BarCode,
            Name = obj.Name,
            Quantity = obj.Quantity
        };
    }

    private static List<ProductAggregate> ToAggregate(List<Product>? obj)
    {
        if (obj?.Any() != true) return new();
        var ret = new List<ProductAggregate>();
        obj.ForEach(x => ret.Add(ToAggregate(x)));
        return ret;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

}
