public interface IInventoryService
{
    Task AddStockAlgo(int productId, int warehouseId, int quantity);
    Task RemoveStockAlgo(int productId, int warehouseId, int quantity);

    Task<IEnumerable<Inventory>> GetByProductAlgo(int productId);
    Task<IEnumerable<Inventory>> GetByWarehouseAlgo(int warehouseId);
}