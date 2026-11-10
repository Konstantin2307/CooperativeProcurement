using CooperativeProcurement.Core.Models;

namespace CooperativeProcurement.Core.Interfaces;

public interface IPurchaseOrderRepository
{
    Task<IEnumerable<PurchaseOrder>> GetAllAsync();
    Task<PurchaseOrder?> GetByIdAsync(Guid id);
    Task<PurchaseOrder> CreateAsync(PurchaseOrder order);
    Task UpdateAsync(PurchaseOrder order);
    Task DeleteAsync(Guid id);
}