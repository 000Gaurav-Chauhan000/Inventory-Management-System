public interface IProductRepository
{
    Task<Product> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Product product);

    Task<IEnumerable<Product>> GetByCategoryAsync(string category);
    Task<IEnumerable<Product>> GetByBrandAsync(string brand);
    Task<Product> GetBySkuAsync(string sku);
    Task<Product> GetByBarcodeAsync(string barcode);
    Task<IEnumerable<Product>> SearchByNameAsync(string name);
}