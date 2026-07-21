using Store.Models;
using Store.Repositories;

namespace Store.Services;

public class PriceService : IPriceService
{
    public readonly IRepository<Price> _priceRepository;

    public PriceService(IRepository<Price> priceRepository)
    {
        _priceRepository = priceRepository;
    }

    public async Task<Price?> GetPriceByIdAsync(int id)
    {
        if (id <= 0) 
            throw new ArgumentException("Invalid price ID.", nameof(id));
        return await _priceRepository.GetByIdAsync(id);
    } 

    public async Task<IEnumerable<Price>> GetAllPricesAsync()
        => await _priceRepository.GetAllAsync();

    public async Task<Price> CreatePriceAsync(Price price)
    {
        if (price == null)
            throw new ArgumentNullException(nameof(price), "Price cannot be null.");
        else if(price.Value < 0)
            throw new ArgumentOutOfRangeException(nameof(price.Value), "Price value cannot be negative.");
        else if(price.Discount < 0 || price.Discount > 100)
            throw new ArgumentOutOfRangeException(nameof(price.Discount), "Discount must be between 0 and 100.");
        await _priceRepository.AddAsync(price);
        await _priceRepository.SaveChangesAsync();
        return price;
    } 

    public async Task UpdatePrice(Price price)
    {
        if (price == null)
            throw new ArgumentNullException(nameof(price), "Price cannot be null.");
        else if(price.PriceID <= 0)
            throw new ArgumentException("Invalid price ID.", nameof(price.PriceID));
        else if(price.Value < 0)
            throw new ArgumentOutOfRangeException(nameof(price.Value), "Price value cannot be negative.");
        else if(price.Discount < 0 || price.Discount > 100)
            throw new ArgumentOutOfRangeException(nameof(price.Discount), "Discount must be between 0 and 100.");

        var data = await _priceRepository.GetByIdAsync(price.PriceID);
        
        if (data == null)
            throw new InvalidOperationException($"Price with ID {price.PriceID} does not exist.");
        data.Discount = price.Discount;
        data.Currency = price.Currency;
        data.DiscountEndDate = price.DiscountEndDate;
        data.Value = price.Value;
        await _priceRepository.SaveChangesAsync();
    } 

    public async Task DeletePrice(int id)
    {
        if(id <= 0)
            throw new ArgumentException("Invalid price ID.", nameof(id));

        Price? price = await _priceRepository.GetByIdAsync(id);
        
        if (price is null)
            throw new InvalidOperationException($"Price with ID {id} does not exist.");
        _priceRepository.Delete(price);
        await _priceRepository.SaveChangesAsync();
    }
}