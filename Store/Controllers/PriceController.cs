using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Services;

namespace Store.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PriceController : ControllerBase
{
    private readonly IPriceService _priceService;

    public PriceController(IPriceService priceService)
    {
        _priceService = priceService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Price>> GetPriceById([FromRoute] int id)
    {
        var price = await _priceService.GetPriceByIdAsync(id);
        if (price == null)
            return NotFound(price);
        return Ok(price);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Price>>> GetPrices()
    {
        var prices = await _priceService.GetAllPricesAsync();
        if(prices == null)
            return NotFound(prices);
        return Ok(prices);
    }

    [HttpPost]
    public async Task<ActionResult<Price>> AddPrice([FromBody] Price price)
    {
        try
        {
            var newPrice = await _priceService.CreatePriceAsync(price);
            if (newPrice == null)
                return BadRequest();
            return CreatedAtAction(nameof(GetPriceById), new { id = newPrice.PriceID }, newPrice);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePrice([FromRoute] int id, [FromBody] Price price)
    {
        Console.WriteLine("Put method");
        try
        {
            if(price == null)
                return BadRequest("No object price received");
            else if(id != price.PriceID)
                return BadRequest("Id from path is different from id in the price object.");
            var existing = await _priceService.GetPriceByIdAsync(id);
            if(existing is null)
                return NotFound();
            await _priceService.UpdatePrice(price);
            return NoContent();
        }
        catch(ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePrice([FromRoute] int id)
    {
        try
        {
            var existing = await _priceService.GetPriceByIdAsync(id);
            if(existing is null)
                return NotFound();

            await _priceService.DeletePrice(id);
            return NoContent();
        }
        catch(ArgumentOutOfRangeException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}