namespace Domain.Command;

public class AddProductCommand
{
    public string Name { get; set; } = string.Empty;
    public string BarCode { get; set; } = string.Empty;
    public int Quantity { get; set; }
}
