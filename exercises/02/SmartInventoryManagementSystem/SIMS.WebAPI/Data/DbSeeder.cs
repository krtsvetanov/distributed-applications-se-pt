using SIMS.WebAPI.Entities;

namespace SIMS.WebAPI.Data;

public static class DbSeeder
{
    public static void SeedData(ApplicationDbContext context)
    {
        // Check if data already exists
        if (context.Categories.Any())
        {
            return; // Database has been seeded
        }

        // Seed Categories with hierarchy - First add parent categories
        var electronics = new Category { Name = "Electronics", ParentCategoryId = null };
        var homeGarden = new Category { Name = "Home & Garden", ParentCategoryId = null };
        
        context.Categories.AddRange(electronics, homeGarden);
        context.SaveChanges();
        
        // Now add child categories with references to parent IDs
        var computers = new Category { Name = "Computers", ParentCategoryId = electronics.Id };
        var mobileDevices = new Category { Name = "Mobile Devices", ParentCategoryId = electronics.Id };
        var furniture = new Category { Name = "Furniture", ParentCategoryId = homeGarden.Id };
        
        context.Categories.AddRange(computers, mobileDevices, furniture);
        context.SaveChanges();

        // Seed Suppliers
        var supplier1 = new Supplier 
        { 
            Name = "TechWorld Inc.", 
            Rating = 4.5m, 
            Country = "USA" 
        };
        var supplier2 = new Supplier 
        { 
            Name = "Global Electronics Ltd.", 
            Rating = 4.8m, 
            Country = "Germany" 
        };
        var supplier3 = new Supplier 
        { 
            Name = "Asian Imports Co.", 
            Rating = 3.9m, 
            Country = "China" 
        };
        
        context.Suppliers.AddRange(supplier1, supplier2, supplier3);
        context.SaveChanges();

        // Seed Warehouses
        var warehouses = new List<Warehouse>
        {
            new Warehouse 
            { 
                Location = "New York, USA", 
                Capacity = 10000 
            },
            new Warehouse 
            { 
                Location = "Berlin, Germany", 
                Capacity = 15000 
            },
            new Warehouse 
            { 
                Location = "Shanghai, China", 
                Capacity = 20000 
            }
        };
        context.Warehouses.AddRange(warehouses);
        context.SaveChanges();

        // Seed Products (20 products with varied data for OData testing)
        var products = new List<Product>
        {
            // Computers
            new Product 
            { 
                Name = "Dell XPS 15 Laptop", 
                SKU = "DELL-XPS15-001", 
                Price = 1299.99m, 
                StockQuantity = 45, 
                CategoryId = computers.Id, 
                SupplierId = supplier1.Id 
            },
            new Product 
            { 
                Name = "HP EliteBook 840", 
                SKU = "HP-EB840-002", 
                Price = 1099.50m, 
                StockQuantity = 32, 
                CategoryId = computers.Id, 
                SupplierId = supplier2.Id 
            },
            new Product 
            { 
                Name = "Lenovo ThinkPad X1", 
                SKU = "LEN-TPX1-003", 
                Price = 1499.00m, 
                StockQuantity = 28, 
                CategoryId = computers.Id, 
                SupplierId = supplier3.Id 
            },
            new Product 
            { 
                Name = "Apple MacBook Pro 16", 
                SKU = "APPL-MBP16-004", 
                Price = 2499.99m, 
                StockQuantity = 15, 
                CategoryId = computers.Id, 
                SupplierId = supplier1.Id 
            },
            new Product 
            { 
                Name = "ASUS ROG Gaming Laptop", 
                SKU = "ASUS-ROG-005", 
                Price = 1799.99m, 
                StockQuantity = 22, 
                CategoryId = computers.Id, 
                SupplierId = supplier3.Id 
            },

            // Mobile Devices
            new Product 
            { 
                Name = "iPhone 15 Pro", 
                SKU = "APPL-IP15P-006", 
                Price = 999.99m, 
                StockQuantity = 85, 
                CategoryId = mobileDevices.Id, 
                SupplierId = supplier1.Id 
            },
            new Product 
            { 
                Name = "Samsung Galaxy S24", 
                SKU = "SAMS-GS24-007", 
                Price = 849.99m, 
                StockQuantity = 92, 
                CategoryId = mobileDevices.Id, 
                SupplierId = supplier2.Id 
            },
            new Product 
            { 
                Name = "Google Pixel 8", 
                SKU = "GOOG-PIX8-008", 
                Price = 699.99m, 
                StockQuantity = 64, 
                CategoryId = mobileDevices.Id, 
                SupplierId = supplier1.Id 
            },
            new Product 
            { 
                Name = "OnePlus 12", 
                SKU = "ONE-OP12-009", 
                Price = 649.99m, 
                StockQuantity = 47, 
                CategoryId = mobileDevices.Id, 
                SupplierId = supplier3.Id 
            },
            new Product 
            { 
                Name = "Xiaomi 14 Pro", 
                SKU = "XIAO-14P-010", 
                Price = 599.99m, 
                StockQuantity = 58, 
                CategoryId = mobileDevices.Id, 
                SupplierId = supplier3.Id 
            },

            // Electronics (Parent Category)
            new Product 
            { 
                Name = "Sony 65\" 4K TV", 
                SKU = "SONY-TV65-011", 
                Price = 899.99m, 
                StockQuantity = 18, 
                CategoryId = electronics.Id, 
                SupplierId = supplier2.Id 
            },
            new Product 
            { 
                Name = "Bose QuietComfort Headphones", 
                SKU = "BOSE-QC-012", 
                Price = 349.99m, 
                StockQuantity = 73, 
                CategoryId = electronics.Id, 
                SupplierId = supplier1.Id 
            },
            new Product 
            { 
                Name = "Canon EOS R6 Camera", 
                SKU = "CANON-R6-013", 
                Price = 2299.99m, 
                StockQuantity = 12, 
                CategoryId = electronics.Id, 
                SupplierId = supplier2.Id 
            },
            new Product 
            { 
                Name = "Logitech MX Master 3 Mouse", 
                SKU = "LOGI-MXM3-014", 
                Price = 99.99m, 
                StockQuantity = 156, 
                CategoryId = electronics.Id, 
                SupplierId = supplier1.Id 
            },

            // Furniture
            new Product 
            { 
                Name = "Ergonomic Office Chair", 
                SKU = "FURN-EOC-015", 
                Price = 299.99m, 
                StockQuantity = 42, 
                CategoryId = furniture.Id, 
                SupplierId = supplier2.Id 
            },
            new Product 
            { 
                Name = "Standing Desk Adjustable", 
                SKU = "FURN-SDA-016", 
                Price = 449.99m, 
                StockQuantity = 35, 
                CategoryId = furniture.Id, 
                SupplierId = supplier3.Id 
            },
            new Product 
            { 
                Name = "Bookshelf 5-Tier", 
                SKU = "FURN-BS5-017", 
                Price = 129.99m, 
                StockQuantity = 67, 
                CategoryId = furniture.Id, 
                SupplierId = supplier3.Id 
            },

            // Home & Garden (Parent Category)
            new Product 
            { 
                Name = "Dyson V15 Vacuum", 
                SKU = "DYSON-V15-018", 
                Price = 649.99m, 
                StockQuantity = 29, 
                CategoryId = homeGarden.Id, 
                SupplierId = supplier2.Id 
            },
            new Product 
            { 
                Name = "Philips Air Purifier", 
                SKU = "PHIL-AP-019", 
                Price = 199.99m, 
                StockQuantity = 51, 
                CategoryId = homeGarden.Id, 
                SupplierId = supplier1.Id 
            },
            new Product 
            { 
                Name = "Smart Thermostat", 
                SKU = "SMART-THERM-020", 
                Price = 179.99m, 
                StockQuantity = 88, 
                CategoryId = homeGarden.Id, 
                SupplierId = supplier1.Id 
            }
        };
        context.Products.AddRange(products);
        context.SaveChanges();

        // Seed StockMovements (sample movements for testing)
        // Get the first few products for stock movements
        var productsList = products.ToList();
        
        var stockMovements = new List<StockMovement>
        {
            new StockMovement 
            { 
                ProductId = productsList[0].Id, 
                Type = "In", 
                Quantity = 50, 
                Date = DateTime.UtcNow.AddDays(-30) 
            },
            new StockMovement 
            { 
                ProductId = productsList[0].Id, 
                Type = "Out", 
                Quantity = 5, 
                Date = DateTime.UtcNow.AddDays(-25) 
            },
            new StockMovement 
            { 
                ProductId = productsList[5].Id, 
                Type = "In", 
                Quantity = 100, 
                Date = DateTime.UtcNow.AddDays(-20) 
            },
            new StockMovement 
            { 
                ProductId = productsList[5].Id, 
                Type = "Out", 
                Quantity = 15, 
                Date = DateTime.UtcNow.AddDays(-15) 
            },
            new StockMovement 
            { 
                ProductId = productsList[10].Id, 
                Type = "In", 
                Quantity = 25, 
                Date = DateTime.UtcNow.AddDays(-10) 
            },
            new StockMovement 
            { 
                ProductId = productsList[10].Id, 
                Type = "Out", 
                Quantity = 7, 
                Date = DateTime.UtcNow.AddDays(-5) 
            },
            new StockMovement 
            { 
                ProductId = productsList[14].Id, 
                Type = "In", 
                Quantity = 60, 
                Date = DateTime.UtcNow.AddDays(-8) 
            },
            new StockMovement 
            { 
                ProductId = productsList[14].Id, 
                Type = "Out", 
                Quantity = 18, 
                Date = DateTime.UtcNow.AddDays(-3) 
            },
            new StockMovement 
            { 
                ProductId = productsList[3].Id, 
                Type = "In", 
                Quantity = 20, 
                Date = DateTime.UtcNow.AddDays(-12) 
            },
            new StockMovement 
            { 
                ProductId = productsList[3].Id, 
                Type = "Out", 
                Quantity = 5, 
                Date = DateTime.UtcNow.AddDays(-2) 
            }
        };
        context.StockMovements.AddRange(stockMovements);
        context.SaveChanges();
    }
}
