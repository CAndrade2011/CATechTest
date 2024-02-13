using Domain.Aggregate;

namespace Domain.DTO;

public class ProductResultDTO
{
    public ProductResultDTO(ProductAggregate product)
    {
        this.Id = product.Id;
        this.Name = product.Name;
        this.BarCode = product.BarCode;
        this.Quantity = product.Quantity;
    }

    public string Id { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string BarCode { get; private set; } = string.Empty;
    public int Quantity { get; private set; }

    public static List<ProductResultDTO>? GenerateListFromAggregates(List<ProductAggregate>? aggregateList)
    {
        return aggregateList?.Select(x => new ProductResultDTO(x)).ToList();
    }
}
