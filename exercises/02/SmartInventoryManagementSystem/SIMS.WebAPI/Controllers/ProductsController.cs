using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using SIMS.WebAPI.Data;
using SIMS.WebAPI.Entities;

namespace SIMS.WebAPI.Controllers;

public class ProductsController : ODataController
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(ApplicationDbContext context, ILogger<ProductsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Get all products with OData query support
    /// Example: GET /odata/Products?$filter=Price gt 500&$expand=Category,Supplier&$orderby=Name
    /// </summary>
    [EnableQuery]
    public IQueryable<Product> Get()
    {
        _logger.LogInformation("Retrieving all products");
        return _context.Products;
    }

    /// <summary>
    /// Get a single product by ID with OData query support
    /// Example: GET /odata/Products(1)?$expand=Category,Supplier,StockMovements
    /// </summary>
    [EnableQuery]
    public SingleResult<Product> Get([FromRoute] int key)
    {
        _logger.LogInformation("Retrieving product with ID: {ProductId}", key);
        var result = _context.Products.Where(p => p.Id == key);
        return SingleResult.Create(result);
    }
}
