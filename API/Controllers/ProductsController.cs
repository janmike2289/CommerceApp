using Core.Entity;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductRepository repository) : ControllerBase
    {
        //private readonly StoreContext context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? brand, string? type, string? sort) 
        {
            //return await repository.GetProductsAsync(); // not gonna work since ActionResult does not work with IReadOnlyList.
            return Ok(await repository.GetProductsAsync(brand,type, sort));
        }

        [HttpGet ("{id:int}")] // api/products/<whatever id the product uses>
        public async Task <ActionResult<Product>> GetIndividualProductById(int id) 
        {
            var product = await repository.GetProductByIdAsync(id);

            if (product == null) { return NotFound(); }

            return product;
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            return Ok(await repository.GetBrandAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            return Ok(await repository.GetTypesAsync());
        }

        [HttpPost]
        public async Task <ActionResult<Product>> CreateProduct(Product product) 
        {
            repository.AddProduct(product);

            if (await repository.SaveChangesAsync())
            {
                return CreatedAtAction("GetIndividualProductById", new { id = product.Id }, product);
            }

            return BadRequest("Problem creating product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product) 
        {
            if (product.Id != id || !ProductExist(id))
                return BadRequest("Product is not available");

            repository.UpdateProduct(product);

            if (await repository.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem updating product");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id) 
        {
            
            var product = await repository.GetProductByIdAsync(id);

            if (product == null) { return NotFound(); }

            repository.DeleteProduct(product);

            if (await repository.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem deleting the product");
        }

        

        private bool ProductExist(int id) 
        {
            return repository.ProductExists(id);
        }

    }
}
