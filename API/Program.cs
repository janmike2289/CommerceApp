using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using API.Middleware;

// ----------------------------------------------Builder (Creates the builder for the web api application)----------------------------------------------------
var builder = WebApplication.CreateBuilder(args);


// ----------------------------------------------Services (Add the services to the application) -------------------------------------

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

//Adds the services for the generic repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddCors(); //add cors service

//Build the web api application
var app = builder.Build();

//-------------------------------------------------------Middleware Pipeline------------------------------------------------------------------------
//Note: The order of the middleware is important. The middleware is executed in the order that it is added to the pipeline.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>(); //custom exception middleware

//Note: should be in the middle of custom middleware and controllers.
//CORS is an HTTP-header based mechanism that allows a server to indicate any origins (domain, scheme, or port) other than its own from which a browser should permit loading resources
//In this case, we are allowing the client (angular) to access resource if it is coming from the origin of 4200
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

try
{
    //create a scope that is outside the dependency injection, so that we can dispose of it after the app has run
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
