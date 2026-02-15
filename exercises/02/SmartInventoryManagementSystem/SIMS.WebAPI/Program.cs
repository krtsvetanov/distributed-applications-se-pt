using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using SIMS.WebAPI.Data;
using SIMS.WebAPI.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    ));

// Add OData with all features enabled
builder.Services.AddControllers()
    .AddOData(options => options
        .Select()           // Enable $select
        .Filter()           // Enable $filter
        .OrderBy()          // Enable $orderby
        .Expand()           // Enable $expand
        .Count()            // Enable $count
        .SetMaxTop(100)     // Set maximum page size to 100
        .AddRouteComponents("odata", GetEdmModel())); // Configure route prefix

// Add services to the container.
builder.Services.AddOpenApi();

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated(); // Creates database if it doesn't exist
        DbSeeder.SeedData(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();

// Build the OData EDM (Entity Data Model)
static Microsoft.OData.Edm.IEdmModel GetEdmModel()
{
    var modelBuilder = new ODataConventionModelBuilder();
    
    // Register entity sets (enables OData endpoints)
    modelBuilder.EntitySet<Product>("Products");
    modelBuilder.EntitySet<Category>("Categories");
    modelBuilder.EntitySet<Supplier>("Suppliers");
    modelBuilder.EntitySet<Warehouse>("Warehouses");
    modelBuilder.EntitySet<StockMovement>("StockMovements");
    
    // Configure entity types
    var product = modelBuilder.EntityType<Product>();
    product.HasKey(p => p.Id);
    
    var category = modelBuilder.EntityType<Category>();
    category.HasKey(c => c.Id);
    
    var supplier = modelBuilder.EntityType<Supplier>();
    supplier.HasKey(s => s.Id);
    
    var warehouse = modelBuilder.EntityType<Warehouse>();
    warehouse.HasKey(w => w.Id);
    
    var stockMovement = modelBuilder.EntityType<StockMovement>();
    stockMovement.HasKey(sm => sm.Id);
    
    return modelBuilder.GetEdmModel();
}
