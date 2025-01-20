

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.Roslyn;
using Core.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Reflection;

public class ProductRepositoryBenchmark
{
    private ProductRepository _repository;

    public class Program() 
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ProductRepositoryBenchmark>();
        }
    }

    //[GlobalSetup]
    //public void Setup()
    //{

    //    var options = new DbContextOptionsBuilder<StoreContext>()
    //        .UseInMemoryDatabase(databaseName: "TestDatabase")
    //        .Options;

    //    var option2 = new DbContextOptionsBuilder<StoreContext>().UseSqlServer(ConfigurationSource.)


    //    var context = new StoreContext(options);
    //    _repository = new ProductRepository(context);

    //    // Seed data
    //    context.Products.Add(new Product { Id = 1, Name = "Product1", Description = "Description1", Price = 10, PictureURL = "url1", Type = "Type1", Brand = "Brand1", QuantityInStock = 100 });
    //    context.Products.Add(new Product { Id = 2, Name = "Product2", Description = "Description2", Price = 20, PictureURL = "url2", Type = "Type2", Brand = "Brand2", QuantityInStock = 200 });
    //    context.SaveChanges();
    //}

    [Benchmark]
    public async Task GetProductByIdAsync()
    {
        await _repository.GetProductByIdAsync(1);
    }

    [Benchmark]
    public async Task GetProductsAsync()
    {
        await _repository.GetProductsAsync(null, null, null);
    }

    [Benchmark]
    public async Task AddProduct()
    {
        var product = new Product { Id = 3, Name = "Product3", Description = "Description3", Price = 30, PictureURL = "url3", Type = "Type3", Brand = "Brand3", QuantityInStock = 300 };
        _repository.AddProduct(product);
        await _repository.SaveChangesAsync();
    }

    [Benchmark]
    public async Task DeleteProduct()
    {
        var product = await _repository.GetProductByIdAsync(1);
        _repository.DeleteProduct(product);
        await _repository.SaveChangesAsync();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<ProductRepositoryBenchmark>();
    }
}
