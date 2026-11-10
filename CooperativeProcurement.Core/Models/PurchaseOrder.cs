namespace CooperativeProcurement.Core.Models;

public class PurchaseOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string SupplierName { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public List<OrderItem> Items { get; set; } = new();
    public decimal TotalAmount => Items.Sum(item => item.TotalPrice);
    public OrderStatus Status { get; set; } = OrderStatus.Draft;
}