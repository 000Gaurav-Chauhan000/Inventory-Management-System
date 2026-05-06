using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<Product> GetByIdAsync(int id)
        => await _context.Products.FindAsync(id);

    public async Task<Product> GetBySkuAsync(string sku)
        => await _context.Products.FirstOrDefaultAsync(p => p.Sku == sku);

    public async Task<Product> GetByBarcodeAsync(string barcode)
        => await _context.Products.FirstOrDefaultAsync(p => p.Barcode == barcode);

    public async Task<IEnumerable<Product>> GetAllAsync()
        => await _context.Products.Where(p => p.IsActive).ToListAsync();

    public async Task<IEnumerable<Product>> GetByCategoryAsync(string category)
        => await _context.Products.Where(p => p.Category == category && p.IsActive).ToListAsync();

    public async Task<IEnumerable<Product>> GetByBrandAsync(string brand)
        => await _context.Products.Where(p => p.Brand == brand && p.IsActive).ToListAsync();

    public async Task<IEnumerable<Product>> SearchByNameAsync(string name)
        => await _context.Products
            .Where(p => p.Name.Contains(name) && p.IsActive)
            .ToListAsync();

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}