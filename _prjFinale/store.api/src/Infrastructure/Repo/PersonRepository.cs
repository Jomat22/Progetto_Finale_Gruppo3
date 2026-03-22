using Microsoft.EntityFrameworkCore;
using store.api.src.Data;
using store.core.src.Domain.Entity.User;
namespace store.api.src.Infrastructure.Repo;

public class PersonRepository(DataContext context)
{
    private readonly DataContext _context = context;

    public async Task<Person?> GetPersonByTaxCode_repo(string codiceFiscale, bool asNoTracking = false) {
        try
        {
            IQueryable<Person> query = _context.People.AsQueryable();
            if (asNoTracking) { query.AsNoTracking(); }

            return await query.FirstOrDefaultAsync(e => e.CodiceFiscale.Equals(codiceFiscale, StringComparison.CurrentCultureIgnoreCase));
        } catch (Exception) { throw; }
    }
    
    public async Task<IEnumerable<Person>> GetAllPerson_repo(bool asNoTracking = false) 
    {
        try 
        {
            IQueryable<Person> query = _context.People.AsQueryable();
            if (asNoTracking) { query = query.AsNoTracking(); }

            return await query.ToListAsync();
        } catch(Exception) { throw; }
    }
    
    public async Task<int> PostPerson_repo(Person person) {
        try
        {
            _context.People.Add(person);
            return await _context.SaveChangesAsync();
        } catch (Exception) { throw; }
    }
    
    public async Task<int> PutPerson_repo(Person person) {
        try
        {
            return await _context.SaveChangesAsync();
        } catch (Exception) { throw; }
    }
    
    public async Task<int> DeletePerson_repo(Person person) {
        try
        {
            _context.Remove(person);
            return await _context.SaveChangesAsync();
        } catch (Exception) { throw; }
    }
}