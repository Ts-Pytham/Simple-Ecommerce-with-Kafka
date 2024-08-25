namespace Ecommerce.Shared.Models.Orders;
public class CreateOrder
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string CustomerName { get; set; } = string.Empty;
}
