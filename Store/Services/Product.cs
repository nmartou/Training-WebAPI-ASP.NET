using Store.Models;
using Store.Repositories;

namespace Store.Services;
public class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;

    public ProductService(IRepository<Product> repository)
    {
        _productRepository = repository;
    }
    public async Task<Product?> GetProductAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Id of product is out of range");
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        if(product is null)
            throw new ArgumentNullException(nameof(product), "Product cannot be null.");
        else if(string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name cannot be empty.", nameof(product.Name));
        else if(string.IsNullOrWhiteSpace(product.Description))
            throw new ArgumentException("Product description cannot be empty.", nameof(product.Description));
        else if(product.Stock < 0)
            throw new ArgumentOutOfRangeException(nameof(product.Stock), "Product stock cannot be negative.");
        else if(product.PriceID <= 0)
            throw new ArgumentOutOfRangeException(nameof(product.PriceID), "Product price ID must be greater than zero.");
        
        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();
        return product;
    }

    public async Task UpdateProduct(Product product)
    {
        if(product is null)
            throw new ArgumentNullException(nameof(product), "Product cannot be null.");
        else if(product.ProductID <= 0)
            throw new ArgumentOutOfRangeException(nameof(product.ProductID), "Product ID must be greater than zero.");
        else if(string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name cannot be empty.", nameof(product.Name));
        else if(string.IsNullOrWhiteSpace(product.Description))
            throw new ArgumentException("Product description cannot be empty.", nameof(product.Description));
        else if(product.Stock < 0)
            throw new ArgumentOutOfRangeException(nameof(product.Stock), "Product stock cannot be negative.");
        else if(product.PriceID <= 0)
            throw new ArgumentOutOfRangeException(nameof(product.PriceID), "Product price ID must be greater than zero.");

        var currProduct = await _productRepository.GetByIdAsync(product.ProductID);
        if(currProduct is null)
            throw new InvalidOperationException($"Product with id {product.ProductID} does not exists.");
        currProduct.Name = product.Name;
        currProduct.Brand = product.Brand;
        currProduct.Description = product.Description;
        currProduct.Stock = product.Stock;
        currProduct.PriceID = product.PriceID;

        await _productRepository.SaveChangesAsync();
    }

    public async Task DeleteProduct(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "Product ID must be greater than zero.");

        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
            throw new InvalidOperationException($"Product with id {id} does not exist.");

        _productRepository.Delete(product);
        await _productRepository.SaveChangesAsync();
    }
}