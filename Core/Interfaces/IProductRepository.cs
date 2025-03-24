using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{   //use ireadonly because knowing that  only use this to read the list and not 
//modify any of the products
    Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort);
    Task<Product?> GetPoductByIdAsync(int id);
    Task<IReadOnlyList<string>> GetBrandAsync();
     Task<IReadOnlyList<string>> GetTypeAsync ();
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    bool ProductExist(int id);
    Task<bool> SaveChangesAsync();

}
