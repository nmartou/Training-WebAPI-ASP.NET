using Store.Models;
using Store.Repositories;

namespace Store.Services;

public class ProductCommandService : IProductCommandService
{
    private readonly IRepository<ProductCommand> _commandRepository;

    public ProductCommandService(IRepository<ProductCommand> commandRepository)
    {
        _commandRepository = commandRepository;
    }

    public async Task<ProductCommand?> GetProductCommandAsync(int id)
    {
        return await _commandRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<ProductCommand>> GetProductCommandsAsync()
    {
        return await _commandRepository.GetAllAsync();
    }

    public async Task<ProductCommand> CreateProductCommandAsync(ProductCommand product)
    {
        if(product.ProductID <= 0)
            throw new ArgumentException("Product is empty");
        else if(product.CommandID <= 0)
            throw new ArgumentException("Command is empty");
        else if(product.Quantity <= 0)
            throw new ArgumentException("Quantity is not greater than zero");
        await _commandRepository.AddAsync(product);
        await _commandRepository.SaveChangesAsync();
        return product;
    }

    public async Task UpdateProductCommandAsync(ProductCommand command)
    {
        var existingCommand = await _commandRepository.GetByIdAsync(command.ProductCommandID);
        if (existingCommand is null)
            throw new ArgumentException($"ProductCommand with id {command.ProductCommandID} does not exist.");
        existingCommand.ProductID = command.ProductID;
        existingCommand.CommandID = command.CommandID;
        existingCommand.Quantity = command.Quantity;
        await _commandRepository.SaveChangesAsync();
    }

    public async Task DeleteProductCommandAsync(int id)
    {
        var existingCommand = await _commandRepository.GetByIdAsync(id);
        if (existingCommand is null)
            throw new ArgumentException($"ProductCommand with id {id} does not exist.");
        _commandRepository.Delete(existingCommand);
        await _commandRepository.SaveChangesAsync();
    }
}