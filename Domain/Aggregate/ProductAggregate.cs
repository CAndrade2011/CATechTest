namespace Domain.Aggregate;

public class ProductAggregate
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string BarCode { get; set; } = string.Empty;
    public int Quantity { get; set; } 
}
