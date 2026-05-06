using Microsoft.EntityFrameworkCore;
public class MovementDbContext : DbContext
{
    public MovementDbContext(DbContextOptions<MovementDbContext> options)
        : base(options)
    {
    }

    public DbSet<StockMovement> StockMovements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StockMovement>()
            .HasKey(x => x.MovementId);
    }
}