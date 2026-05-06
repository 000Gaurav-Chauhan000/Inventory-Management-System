public class InventoryServiceImpl : IInventoryService
{
    private readonly IInventoryRepository _repo;

    public InventoryServiceImpl(IInventoryRepository repo)
    {
        _repo = repo;
    }

    public async Task AddStockAlgo(int productId, int warehouseId, int quantity)
    {
        var inventory = await _repo.GetAsync(productId, warehouseId);

        if (inventory == null)
        {
            inventory = new Inventory
            {
                ProductId = productId,
                WarehouseId = warehouseId,
                Quantity = quantity
            };

            await _repo.AddAsync(inventory);
        }
        else
        {
            inventory.Quantity += quantity;
            await _repo.UpdateAsync(inventory);
        }

        await _repo.AddMovementAsync(new StockMovement
        {
            ProductId = productId,
            WarehouseId = warehouseId,
            Quantity = quantity,
            Type = "IN"
        });
    }

    public async Task RemoveStockAlgo(int productId, int warehouseId, int quantity)
    {
        var inventory = await _repo.GetAsync(productId, warehouseId);

        if (inventory == null || inventory.Quantity < quantity)
            throw new Exception("Insufficient stock");

        inventory.Quantity -= quantity;
        await _repo.UpdateAsync(inventory);

        await _repo.AddMovementAsync(new StockMovement
        {
            ProductId = productId,
            WarehouseId = warehouseId,
            Quantity = quantity,
            Type = "OUT"
        });
    }

    public async Task<IEnumerable<Inventory>> GetByProductAlgo(int productId)
        => await _repo.GetByProductAsync(productId);

    public async Task<IEnumerable<Inventory>> GetByWarehouseAlgo(int warehouseId)
        => await _repo.GetByWarehouseAsync(warehouseId);
}