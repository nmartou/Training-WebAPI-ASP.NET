using Store.Models;

namespace Store.Services;
public interface IProductService
{
    Task<Product?> GetProductAsync(int id);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> CreateProductAsync(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(int id);
}