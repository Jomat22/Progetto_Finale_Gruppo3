using Microsoft.EntityFrameworkCore;
using store.api.src.Data;
using store.core.src.Domain.Entity.Catalog;
namespace store.api.src.Infrastructure.Repo;

public class ProductRepository(DataContext context)
{
    private readonly DataContext _context = context;

    public async Task<Product?> GetProductById_repo(int id, bool asNoTracking = true) 
    {
        try
        {
            IQueryable<Product> query = _context.Products.AsQueryable();
            if (asNoTracking) { query.AsNoTracking(); }

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        } catch (Exception) { throw; }
    }

    public async Task<Product?> GetProductBySku_repo(string sku, bool asNoTracking = true) 
    {
        try
        {
            IQueryable<Product> query = _context.Products.AsQueryable();
            if (asNoTracking) { query.AsNoTracking(); }

            return await query.FirstOrDefaultAsync(e => e.Sku.ToLower() == sku.ToLower());
        } catch (Exception) { throw; }
    }
    
    public async Task<IEnumerable<Product>> GetAllProduct_repo(bool asNoTracking = true)
    {
        try 
        {
            IQueryable<Product> query = _context.Products.AsQueryable();
            if (asNoTracking) { query = query.AsNoTracking(); }

            return await query.ToListAsync();
        } catch(Exception) { throw; }
    }
    
    public async Task<int> PostProduct_repo(Product product) 
    {
        try
        {
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        } catch (Exception) { throw; }
    }
    
    public async Task<int> PutProduct_repo() 
    {
        try
        {
            return await _context.SaveChangesAsync();
        } catch (Exception) { throw; }
    }
    
    public async Task<int> DeleteProduct_repo(Product product) {
        try
        {
            _context.Remove(product);
            return await _context.SaveChangesAsync();
        } catch (Exception) { throw; }
    }
}