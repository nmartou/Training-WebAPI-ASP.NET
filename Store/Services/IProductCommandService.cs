using Store.Models;

namespace Store.Services;

public interface IProductCommandService
{
    Task<ProductCommand?> GetProductCommandAsync(int id);
    Task<IEnumerable<ProductCommand>> GetProductCommandsAsync();
    Task<ProductCommand> CreateProductCommandAsync(ProductCommand command);
    Task UpdateProductCommandAsync(ProductCommand command);
    Task DeleteProductCommandAsync(int id);
}