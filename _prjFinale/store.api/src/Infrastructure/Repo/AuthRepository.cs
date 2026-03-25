using Microsoft.EntityFrameworkCore;
using store.api.src.Data;
using store.core.src.Domain.Entity.User;
namespace store.api.src.Infrastructure.Repo;

public class AuthRepository(DataContext context)
{
    private readonly DataContext _context = context;

    // Per verificare se id delle persona è già in utilizzo
    public async Task<Employee?> GetAuthByCompanyMailAndPass_repo(string emailAziendale, string password, bool asNoTracking = true) 
    {
        try
        {
            IQueryable<Employee> query = _context.Employees.AsQueryable();
            if (asNoTracking) { query.AsNoTracking(); }

            return await query.FirstOrDefaultAsync(e => e.EmailAziendale.ToLower() == emailAziendale && e.Password.ToLower() == password);
        } catch (Exception) { throw; }
    }
}