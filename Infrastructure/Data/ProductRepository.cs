using Core.Entity;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace Infrastructure.Data
{
    public class ProductRepository(StoreContext context) : IProductRepository
    {

        public void AddProduct(Product product)
        {
            context.Products.Add(product); 
        }

        public void DeleteProduct(Product product)
        {
            context.Products.Remove(product);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            //var Product = await context.Products.FindAsync(id);

            //if (ProductExists(id))
            //{
            //    return await context.Products.FindAsync(id);
            //}
            //else
            //{
            //    return null;
            //}

            return await context.Products.FindAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await context.Products.ToListAsync();
        }

        public bool ProductExists(int id)
        {
            return context.Products.Any(e => e.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void UpdateProduct(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
        }
    }
}
