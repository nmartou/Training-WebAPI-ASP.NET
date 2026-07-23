using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Services;

namespace Store.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct([FromRoute] int id)
    {
        try
        {
            Product? product = await  _productService.GetProductAsync(id);
            if(product is null)
                return NotFound();
            return Ok(product);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        IEnumerable<Product> products = await _productService.GetAllProductsAsync();
        if(products is null)
            return NotFound();
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
        try
        {
            product = await _productService.CreateProductAsync(product);
            if(product is null)
                return BadRequest("Product is empty");
            return Created("/api/product/", product);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> ModifyProduct([FromRoute] int id, [FromBody] Product product)
    {
        if (id != product.ProductID)
            return BadRequest($"Path id does not match with the product id {product.ProductID}");
        
        try
        {
            await _productService.UpdateProduct(product);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct([FromRoute] int id)
    {
        try
        {
            await _productService.DeleteProduct(id);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return NoContent();
    }
}