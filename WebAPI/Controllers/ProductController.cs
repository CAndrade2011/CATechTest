using Domain.Aggregate;
using Domain.Command;
using Domain.DTO;
using Domain.Service;
using Infra.DataFromMongo.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    // GET: products
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        List<ProductAggregate> products;
        try
        {
            products = await _productService.GetAllProductsAsync();
        }
        catch (Exception ex) when (ex is AggregateException)
        {
            return UnprocessableEntity(ex);
        }
        if (products == null)
        {
            return NotFound();
        }
        return Ok(ProductResultDTO.GenerateListFromAggregates(products));
    }

    // GET: products/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById([FromRoute] string id)
    {
        if (string.IsNullOrWhiteSpace(id)) return BadRequest();
        ProductAggregate product;
        try
        {
            product = await _productService.GetProductByIdAsync(id);
        }
        catch (Exception ex) when (ex is AggregateException)
        {
            return UnprocessableEntity(ex);
        }
        if (product == null)
        {
            return NotFound();
        }
        return Ok(new ProductResultDTO(product));
    }

    // POST: products
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] AddProductCommand product)
    {
        if (product == null) return BadRequest();
        bool createdProduct;
        try
        {
            createdProduct = await _productService.CreateProductAsync(new ProductAggregate
            {
                BarCode = product.BarCode,
                Name = product.Name,
                Quantity = product.Quantity
            });
        }
        catch (Exception ex) when (ex is AggregateException)
        {
            return UnprocessableEntity(ex);
        }
        return Ok(new { success = createdProduct });
    }

    // PUT: products/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] string id, [FromBody] UpdProductCommand product)
    {
        if (product == null) return BadRequest();
        if (string.IsNullOrWhiteSpace(id)) return BadRequest();
        bool updatedProduct;
        try
        {
            updatedProduct = await _productService.UpdateProductAsync(new ProductAggregate
            {
                BarCode = product.BarCode,
                Name = product.Name,
                Quantity = product.Quantity,
                Id = id
            });
        }
        catch (Exception ex) when (ex is AggregateException)
        {
            return UnprocessableEntity(ex);
        }
        if (!updatedProduct)
        {
            return NotFound();
        }
        return Ok(new { success = updatedProduct });
    }

    // DELETE: products/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] string id)
    {
        if (string.IsNullOrWhiteSpace(id)) return BadRequest();
        bool isDeleted;
        try
        {
            isDeleted = await _productService.DeleteProductAsync(id);
        }
        catch (Exception ex) when (ex is AggregateException)
        {
            return UnprocessableEntity(ex);
        }
        if (!isDeleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
