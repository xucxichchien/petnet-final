using System;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository repo) : ControllerBase
{

   
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, 
    string? type, string? sort)
    {
            return Ok( await repo.GetProductsAsync(brand, type, sort));
    }
    [HttpGet("{id:int}")] //api/produict/5
    public async Task<ActionResult<Product>> GetProduct(int id) 
    {
        var product = await repo.GetPoductByIdAsync(id);
        if (product == null) return NotFound();
        return product;
    }
    
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.AddProduct(product);
        if(await repo.SaveChangesAsync()) {
            return CreatedAtAction("GetProduct", new {id = product.Id}, product);
        }
        return BadRequest("Cannot create the product");
    }

    //edit product
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product){
        if (product.Id != id || !ProductExists(id))
        return BadRequest("Cannot update this product");

        repo.UpdateProduct(product);
        if(await repo.SaveChangesAsync()) {
            return NoContent();
        }
       
       return BadRequest("Error occurs");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult>DeleteProduct(int id){
        var product = await repo.GetPoductByIdAsync(id);

        if (product == null ) return NotFound();

        repo.DeleteProduct(product);

        if ( await repo.SaveChangesAsync()) {
            return NoContent();
        }
        return BadRequest("Can't delete the product at the moment");
    }

    [HttpGet("brands")]
    public async Task <ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        return Ok(await repo.GetBrandAsync());
    }

    [HttpGet("types")]
    public async Task <ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        return Ok(await repo.GetTypeAsync());
    }
    private bool ProductExists(int id){
        return repo.ProductExist(id);
    }
}
