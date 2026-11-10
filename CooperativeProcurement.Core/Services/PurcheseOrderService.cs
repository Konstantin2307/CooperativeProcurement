using CooperativeProcurement.Core.Interfaces;
using CooperativeProcurement.Core.Models;

namespace CooperativeProcurement.Core.Services;

public class PurchaseOrderService
{
    private readonly IPurchaseOrderRepository _repository;

    public PurchaseOrderService(IPurchaseOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<PurchaseOrder> CreateOrderAsync(string supplierName, List<OrderItem> items)
    {
        if (string.IsNullOrWhiteSpace(supplierName))
            throw new ArgumentException("Имя поставщика обязательно", nameof(supplierName));

        var order = new PurchaseOrder
        {
            SupplierName = supplierName,
            Items = items,
            Status = OrderStatus.Draft
        };

        return await _repository.CreateAsync(order);
    }

    public async Task<IEnumerable<PurchaseOrder>> GetAllOrdersAsync()
    {
        return await _repository.GetAllAsync();
    }
}