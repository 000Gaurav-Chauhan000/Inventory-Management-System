using Microsoft.EntityFrameworkCore;

public class PurchaseDbContext : DbContext
{
    public PurchaseDbContext(DbContextOptions<PurchaseDbContext> options)
        : base(options) { }

    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
}