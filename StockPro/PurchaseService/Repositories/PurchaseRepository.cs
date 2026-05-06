using Microsoft.EntityFrameworkCore;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly PurchaseDbContext _context;

    public PurchaseRepository(PurchaseDbContext context)
    {
        _context = context;
    }

    public async Task<PurchaseOrder?> GetByIdAsync(int id)
    {
        return await _context.PurchaseOrders
            .Include(p => p.Items)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<PurchaseOrder>> GetAllAsync()
    {
        return await _context.PurchaseOrders
            .Include(p => p.Items)
            .ToListAsync();
    }

    public async Task AddAsync(PurchaseOrder order)
    {
        await _context.PurchaseOrders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PurchaseOrder order)
    {
        _context.PurchaseOrders.Update(order);
        await _context.SaveChangesAsync();
    }
}