using Core.Entity;
using Newtonsoft.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context) 
        {
            if (!context.Products.Any())
            {
                //var basepath = AppDomain.CurrentDomain.BaseDirectory;
                var relativePath = @"C:\01 - Work Files\02 - Angular-Core Projects\Angular Projects\CommerceApp\Code\CommerceApp\Infrastructure\Data\SeedData";
                var fileName = "products.json";
                //var fullPath = Path.Combine(basepath, relativePath, fileName);
                var fullPath = Path.Combine(relativePath, fileName);

                var productsData = await File.ReadAllTextAsync(fullPath);
                //var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                var products = JsonConvert.DeserializeObject<List<Product>>(productsData);
                //foreach (var product in products)
                //{
                //    context.Products.Add(product);
                //}
                //await context.SaveChangesAsync();

                if (products == null) return;

                context.Products.AddRange(products);

                await context.SaveChangesAsync();
            }
        }
    }
}
