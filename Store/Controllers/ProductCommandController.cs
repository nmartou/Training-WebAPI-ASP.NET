using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Services;

namespace Store.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductCommandController : ControllerBase
{
    private readonly IProductCommandService _productCommandService;

    public ProductCommandController(IProductCommandService productCommandService)
    {
        _productCommandService = productCommandService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductCommand>> GetProductCommandById([FromRoute] int id)
    {
        try
        {
            ProductCommand? productCommand = await _productCommandService.GetProductCommandAsync(id);
            if (productCommand is null)
                return NotFound("ProductCommand not found.");
            return Ok(productCommand);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductCommand>>> GetProductCommands()
    {
        try
        {
            IEnumerable<ProductCommand> productCommands = await _productCommandService.GetProductCommandsAsync();
            return Ok(productCommands);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ProductCommand>> CreateProductCommand([FromBody] ProductCommand productCommand)
    {
        try
        {
            ProductCommand createdProductCommand = await _productCommandService.CreateProductCommandAsync(productCommand);
            return Created("/api/productcommand/", createdProductCommand);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProductCommand([FromRoute] int id, [FromBody] ProductCommand productCommand)
    {
        try
        {
            if (id != productCommand.ProductCommandID)
                return BadRequest($"Path id does not match with the product command id {productCommand.ProductCommandID}");
            await _productCommandService.UpdateProductCommandAsync(productCommand);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProductCommand([FromRoute] int id)
    {
        try
        {
            var productCommand = await _productCommandService.GetProductCommandAsync(id);
            if (productCommand is null)
                return NotFound("ProductCommand not found.");
            await _productCommandService.DeleteProductCommandAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}