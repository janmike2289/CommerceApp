using API.RequestHelpers;
using Core.Entity;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class ProductsController(IGenericRepository<Product> repository) : BaseAPIController
    {
        //private readonly StoreContext context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] ProductSpecParams specParams) 
        {
            //return await repository.GetProductsAsync(); // not gonna work since ActionResult does not work with IReadOnlyList.
            //return Ok(await repository.GetProductsAsync(brand,type, sort));

            var spec = new ProductSpecification(specParams);

            //var products = await repository.ListAsync(spec);
            //var count = await repository.CountAsync(spec);

            //var pagination = new Pagination<Product>(specParams.PageIndex, specParams.PageSize, count, products);

            //return Ok(await repository.GetAllAsync());

            return await CreatePagedResult(repository, spec, specParams.PageIndex, specParams.PageSize);
        }

        [HttpGet ("{id:int}")] // api/products/<whatever id the product uses>
        public async Task <ActionResult<Product>> GetIndividualProductById(int id) 
        {
            var product = await repository.GetByIdAsync(id);

            if (product == null) { return NotFound(); }

            return product;
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            //TODO: Implement method
            
            //return Ok(await repository.GetBrandAsync());

            var spec = new BrandListSpecification();
            return Ok(await repository.ListAsync(spec));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            //TODO: Implement method
            
            //return Ok(await repository.GetTypesAsync());

            var spec = new TypeListSpecification();
            return Ok(await repository.ListAsync(spec));
        }

        [HttpPost]
        public async Task <ActionResult<Product>> CreateProduct(Product product) 
        {
            repository.Add(product);

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

            repository.Update(product);

            if (await repository.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem updating product");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id) 
        {
            
            var product = await repository.GetByIdAsync(id);

            if (product == null) { return NotFound(); }

            repository.Remove(product);

            if (await repository.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem deleting the product");
        }

        private bool ProductExist(int id) 
        {
            return repository.Exists(id);
        }

    }
}
