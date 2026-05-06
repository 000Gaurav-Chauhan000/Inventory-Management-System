public interface IMovementRepository
{
    Task AddAsync(StockMovement movement);
    Task<IEnumerable<StockMovement>> GetByProductAsync(int productId);
    Task<IEnumerable<StockMovement>> GetByWarehouseAsync(int warehouseId);
    Task<IEnumerable<StockMovement>> GetAllAsync();
    Task<int> GetLatestBalance(int productId, int warehouseId);
}