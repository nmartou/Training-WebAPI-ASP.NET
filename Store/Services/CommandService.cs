using Store.Models;
using Store.Repositories;

namespace Store.Services;

public class CommandService : ICommandService
{
    private readonly IRepository<Command> _commandRepository;

    public CommandService(IRepository<Command> commandRepository)
    {
        _commandRepository = commandRepository;
    }

    public async Task<Command?> GetCommandAsync(int id)
    {
        return await _commandRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Command>> GetCommandsAsync()
    {
        return await _commandRepository.GetAllAsync();
    }

    public async Task<Command> CreateCommandAsync(Command command)
    {
        try
        {
            if(command.SellerID <= 0)
                throw new ArgumentException("Seller is empty");
            else if(command.BuyerID <= 0)
                throw new ArgumentException("Buyer is empty");
            await _commandRepository.AddAsync(command);
            await _commandRepository.SaveChangesAsync();
            return command;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while creating the command.", ex);
        }
    }

    public async Task UpdateCommandAsync(Command command)
    {
        try
        {
            var existingCommand = await _commandRepository.GetByIdAsync(command.CommandID);
            if (existingCommand is null)
                throw new ArgumentException($"Command with id {command.CommandID} does not exist.");
            existingCommand.SellerID = command.SellerID;
            existingCommand.BuyerID = command.BuyerID;
            existingCommand.IsPaid = command.IsPaid;
            existingCommand.TotalPrice = command.TotalPrice;
            existingCommand.TotalQuanity = command.TotalQuanity;
            await _commandRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the command.", ex);
        }
    }

    public async Task DeleteCommandAsync(int id)
    {
        try
        {
            var command = await _commandRepository.GetByIdAsync(id);
            if (command is null)
                throw new ArgumentException($"Command with id {id} does not exist.");
            _commandRepository.Delete(command);
            await _commandRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting the command.", ex);
        }
    }
}