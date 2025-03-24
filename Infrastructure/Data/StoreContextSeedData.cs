using System;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeedData
{
    public static async Task SeedAsync(StoreContext context)
    {
        if (!context.Products.Any())
        {
            var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/Seed/products.json");

            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            if(products == null) 

            return;

            context.Products.AddRange(products);

            await context.SaveChangesAsync();
        }
    }
}
