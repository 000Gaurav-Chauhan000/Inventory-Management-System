public interface IPurchaseRepository
{
    Task<PurchaseOrder?> GetByIdAsync(int id);
    Task<IEnumerable<PurchaseOrder>> GetAllAsync();

    Task AddAsync(PurchaseOrder order);
    Task UpdateAsync(PurchaseOrder order);
}