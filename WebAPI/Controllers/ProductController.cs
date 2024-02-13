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
        var products = await _productService.GetAllProductsAsync();
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
        var product = await _productService.GetProductByIdAsync(id);
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
        var createdProduct = await _productService.CreateProductAsync(new ProductAggregate
        {
            BarCode = product.BarCode,
            Name = product.Name,
            Quantity = product.Quantity
        });
        // TODO: fix create action to improve RestFulAPI
        //return CreatedAtAction(nameof(GetProductById), new { success = createdProduct }, createdProduct);
        return Ok(new { success = createdProduct });
    }

    // PUT: products/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] string id, [FromBody] UpdProductCommand product)
    {
        if (product == null) return BadRequest();
        if (string.IsNullOrWhiteSpace(id)) return BadRequest();
        var updatedProduct = await _productService.UpdateProductAsync(new ProductAggregate
        {
            BarCode = product.BarCode,
            Name = product.Name,
            Quantity = product.Quantity,
            Id = id
        });
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
        var isDeleted = await _productService.DeleteProductAsync(id);
        if (!isDeleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
