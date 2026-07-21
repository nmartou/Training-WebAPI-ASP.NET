using Store.Models;

namespace Store.Services;
public interface IPriceService
{
    Task<Price?> GetPriceByIdAsync(int id);
    Task<IEnumerable<Price>> GetAllPricesAsync();
    Task<Price> CreatePriceAsync(Price price);
    Task UpdatePrice(Price price);
    Task DeletePrice(int id);
}