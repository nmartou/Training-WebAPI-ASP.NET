using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Services;

namespace Store.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommandController : ControllerBase
{
    private readonly ICommandService _commandService;
    public CommandController(ICommandService service)
    {
        _commandService = service;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Command>> GetCommandById([FromRoute] int id)
    {
        try
        {
            Command? command = await _commandService.GetCommandAsync(id);
            if (command is null)
                return NotFound("Command not found.");
            return Ok(command);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Command>>> GetCommands()
    {
        Console.WriteLine("\n Hereeeee \n");
        try
        {
            IEnumerable<Command> commands = await _commandService.GetCommandsAsync();
            return Ok(commands);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Command>> CreateCommand([FromBody] Command command)
    {
        try
        {
            Command createdCommand = await _commandService.CreateCommandAsync(command);
            return Created("/api/command/", createdCommand);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Command>> UpdateCommand([FromRoute] int id, [FromBody] Command command)
    {
        try
        {
            if(id != command.CommandID)
                return BadRequest("Id from path is different from id in the command object.");
            await _commandService.UpdateCommandAsync(command);
            return Ok(command);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCommand([FromRoute] int id)
    {
        try
        {
            await _commandService.DeleteCommandAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}