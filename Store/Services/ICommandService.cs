using Store.Models;

namespace Store.Services;

public interface ICommandService
{
    Task<Command?> GetCommandAsync(int id);
    Task<IEnumerable<Command>> GetCommandsAsync();
    Task<Command> CreateCommandAsync(Command command);
    Task UpdateCommandAsync(Command command);
    Task DeleteCommandAsync(int id);
}