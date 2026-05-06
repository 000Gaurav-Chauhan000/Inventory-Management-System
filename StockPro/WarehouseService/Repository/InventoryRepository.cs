using Microsoft.EntityFrameworkCore;

public class InventoryRepository : IInventoryRepository
{
    private readonly WarehouseDbContext _context;

    public InventoryRepository(WarehouseDbContext context)
    {
        _context = context;
    }

    public async Task<Inventory?> GetAsync(int productId, int warehouseId)
    {
        return await _context.Inventories
            .FirstOrDefaultAsync(i => i.ProductId == productId && i.WarehouseId == warehouseId);
    }

    public async Task<IEnumerable<Inventory>> GetByProductAsync(int productId)
    {
        return await _context.Inventories
            .Where(i => i.ProductId == productId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Inventory>> GetByWarehouseAsync(int warehouseId)
    {
        return await _context.Inventories
            .Where(i => i.WarehouseId == warehouseId)
            .ToListAsync();
    }

    public async Task AddAsync(Inventory inventory)
    {
        await _context.Inventories.AddAsync(inventory);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Inventory inventory)
    {
        _context.Inventories.Update(inventory);
        await _context.SaveChangesAsync();
    }

    public async Task AddMovementAsync(StockMovement movement)
    {
        await _context.StockMovements.AddAsync(movement);
        await _context.SaveChangesAsync();
    }
}