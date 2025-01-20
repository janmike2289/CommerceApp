// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Core.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;


//public class Benchmark 
//{
//    private static ProductRepository _repository;

//    [MemoryDiagnoser]
//    public class Program()
//    {


//        public static void Main(string[] args)
//        {
//            var summary = BenchmarkRunner.Run<Benchmark>();

//            var options = new DbContextOptionsBuilder<StoreContext>().UseSqlServer("Server=localhost, 1433; Database=ECommDB; User Id=SA; Password=Password@1; TrustServerCertificate=True;").Options;

//            var context = new StoreContext(options);
//            _repository = new ProductRepository(context);

//            // Seed data
//            context.Products.Add(new Product { Id = 1, Name = "Product1", Description = "Description1", Price = 10, PictureURL = "url1", Type = "Type1", Brand = "Brand1", QuantityInStock = 100 });
//            context.Products.Add(new Product { Id = 2, Name = "Product2", Description = "Description2", Price = 20, PictureURL = "url2", Type = "Type2", Brand = "Brand2", QuantityInStock = 200 });
//            context.SaveChanges();
//        }
//    }


//    [Benchmark]
//    public async Task GetProductByIdAsync()
//    {
//        await _repository.GetProductByIdAsync(1);
//    }

//    [Benchmark]
//    public async Task GetProductsAsync()
//    {
//        await _repository.GetProductsAsync(null, null, null);
//    }

//    [Benchmark]
//    public async Task AddProduct()
//    {
//        var product = new Product { Id = 3, Name = "Product3", Description = "Description3", Price = 30, PictureURL = "url3", Type = "Type3", Brand = "Brand3", QuantityInStock = 300 };
//        _repository.AddProduct(product);
//        await _repository.SaveChangesAsync();
//    }

//    [Benchmark]
//    public async Task DeleteProduct()
//    {
//        var product = await _repository.GetProductByIdAsync(1);
//        _repository.DeleteProduct(product);
//        await _repository.SaveChangesAsync();
//    }
//}


//namespace StringBenchmarks;

[MemoryDiagnoser]
public class Benchmarks
{
    [Params(5, 50, 500)] //<-- This is a parameter attribute
    public int N { get; set; }

    [Benchmark(Baseline = true)] // <-- this is the baseline
    public string StringJoin()
    {
        return string.Join(", ", Enumerable.Range(0, N).Select(i => i.ToString()));
    }

    [Benchmark]
    public string StringBuilder()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < N; i++)
        {
            sb.Append(i);
            sb.Append(", ");
        }

        return sb.ToString();
    }
}


public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<Benchmarks>();
    }
}


