public interface IInventoryRepository
{
    Task<Inventory?> GetAsync(int productId, int warehouseId);
    Task<IEnumerable<Inventory>> GetByProductAsync(int productId);
    Task<IEnumerable<Inventory>> GetByWarehouseAsync(int warehouseId);

    Task AddAsync(Inventory inventory);
    Task UpdateAsync(Inventory inventory);

    Task AddMovementAsync(StockMovement movement);
}