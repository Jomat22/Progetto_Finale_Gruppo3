using Microsoft.EntityFrameworkCore;
using store.api.src.Data;
using store.core.src.Domain.Entity.User;
namespace store.api.src.Infrastructure.Repo;

public class EmployeeRepository(DataContext context)
{
    private readonly DataContext _context = context;

    // Per verificare se id delle persona è già in utilizzo
    public async Task<Employee?> GetEmployeeByPersonId_repo(int personId, bool asNoTracking = true) 
    {
        try
        {
            IQueryable<Employee> query = _context.Employees.AsQueryable();
            if (asNoTracking) { query.AsNoTracking(); }

            return await query.FirstOrDefaultAsync(e => e.PersonId == personId);
        } catch (Exception) { throw; }
    }

    public async Task<Employee?> GetEmployeeByCompanyEmail_repo(string emailAziendale, bool asNoTracking = true) 
    {
        try
        {
            IQueryable<Employee> query = _context.Employees.AsQueryable();
            if (asNoTracking) { query.AsNoTracking(); }

            return await query.FirstOrDefaultAsync(e => e.EmailAziendale.ToLower() == emailAziendale.ToLower());
        } catch (Exception) { throw; }
    }

    public async Task<Employee?> GetEmployeeByMCode_repo(string codiceMeccanografico, bool asNoTracking = true) 
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