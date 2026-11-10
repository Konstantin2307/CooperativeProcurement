using CooperativeProcurement.Core.Interfaces;
using CooperativeProcurement.Core.Models;
using System.Text.Json;

namespace CooperativeProcurement.Infrastructure.Repositories;

public class JsonPurchaseOrderRepository : IPurchaseOrderRepository
{
    private readonly string _filePath = "orders.json";
    private List<PurchaseOrder> _orders = new();

    public JsonPurchaseOrderRepository()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            _orders = JsonSerializer.Deserialize<List<PurchaseOrder>>(json) ?? new();
        }
    }

    private void Save()
    {
        var json = JsonSerializer.Serialize(_orders, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public Task<IEnumerable<PurchaseOrder>> GetAllAsync() => Task.FromResult<IEnumerable<PurchaseOrder>>(_orders);

    public Task<PurchaseOrder?> GetByIdAsync(Guid id) => Task.FromResult(_orders.FirstOrDefault(o => o.Id == id));

    public Task<PurchaseOrder> CreateAsync(PurchaseOrder order)
    {
        _orders.Add(order);
        Save();
        return Task.FromResult(order);
    }

    public Task UpdateAsync(PurchaseOrder order)
    {
        var existing = _orders.FirstOrDefault(o => o.Id == order.Id);
        if (existing != null)
        {
            _orders.Remove(existing);
            _orders.Add(order);
            Save();
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        if (order != null)
        {
            _orders.Remove(order);
            Save();
        }
        return Task.CompletedTask;
    }
}