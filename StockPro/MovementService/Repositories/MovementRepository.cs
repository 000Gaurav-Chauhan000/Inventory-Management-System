using Microsoft.EntityFrameworkCore;
public class MovementRepository : IMovementRepository
{
    private readonly MovementDbContext _context;

    public MovementRepository(MovementDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(StockMovement movement)
    {
        await _context.StockMovements.AddAsync(movement);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<StockMovement>> GetByProductAsync(int productId)
    {
        return await _context.StockMovements
            .Where(x => x.ProductId == productId)
            .ToListAsync();
    }

    public async Task<IEnumerable<StockMovement>> GetByWarehouseAsync(int warehouseId)
    {
        return await _context.StockMovements
            .Where(x => x.WarehouseId == warehouseId)
            .ToListAsync();
    }

    public async Task<IEnumerable<StockMovement>> GetAllAsync()
    {
        return await _context.StockMovements.ToListAsync();
    }

    public async Task<int> GetLatestBalance(int productId, int warehouseId)
    {
        var last = await _context.StockMovements
            .Where(x => x.ProductId == productId && x.WarehouseId == warehouseId)
            .OrderByDescending(x => x.MovementDate)
            .FirstOrDefaultAsync();

        return last?.BalanceAfter ?? 0;
    }
}