using Microsoft.EntityFrameworkCore;
using store.api.src.Data;
using store.core.src.Domain.Entity.User;
namespace store.api.src.Infrastructure.Repo;

public class EmployeeRepository(DataContext context)
{
    private readonly DataContext _context = context;

    public async Task<Employee?> GetEmployeeByMCode_repo(string codiceMeccanografico, bool asNoTracking = false) 
    {
        try
        {
            IQueryable<Employee> query = _context.Employees.AsQueryable();
            if (asNoTracking) { query.AsNoTracking(); }

            return await query.FirstOrDefaultAsync(e => e.CodiceMeccanografico.ToLower() == codiceMeccanografico.ToLower());
        } catch (Exception) { throw; }
    }
    
    public async Task<IEnumerable<Employee>> GetAllEmployee_repo(bool asNoTracking = true)
    {
        try 
        {
            IQueryable<Employee> query = _context.Employees.AsQueryable();
            if (asNoTracking) { query = query.AsNoTracking(); }

            return await query.ToListAsync();
        } catch(Exception) { throw; }
    }
    
    public async Task<int> PostEmployee_repo(Employee employee) 
    {
        try
        {
            _context.Employees.Add(employee);
            return await _context.SaveChangesAsync();
        } catch (Exception) { throw; }
    }
    
    public async Task<int> PutEmployee_repo() 
    {
        try
        {
            return await _context.SaveChangesAsync();
        } catch (Exception) { throw; }
    }
    
    public async Task<int> DeleteEmployee_repo(Employee employee) {
        try
        {
            _context.Remove(employee);
            return await _context.SaveChangesAsync();
        } catch (Exception) { throw; }
    }
}