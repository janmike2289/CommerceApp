using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), opt => opt.EnableRetryOnFailure()); //gets the db connection from the app settings configuration
}
);

builder.Services.AddScoped<IProductRepository, ProductRepository>(); //scoped per https request
//builder.Services.AddTransient<IProductRepository, ProductRepository>(); //scope on the method
//builder.Services.AddSingleton<IProductRepository, ProductRepository>(); //singleton for the app lifetime

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    //create a scope that is outside of the dependency injection, so that we can dispose of it after the app has run
    using var scope = app.Services.CreateScope(); 
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();
    
    await context.Database.MigrateAsync(); //apply any pending migrations
    await StoreContextSeed.SeedAsync(context); //seed the database
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    throw;
}

app.Run();
