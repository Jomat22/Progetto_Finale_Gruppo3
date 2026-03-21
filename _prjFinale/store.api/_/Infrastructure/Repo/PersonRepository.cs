using Microsoft.EntityFrameworkCore;
using store.api._.Data;
using store.core._.Domain.Entity.User;
namespace store.api._.Infrastructure.Repo;

public class PersonRepository(DataContext context)
{
    private readonly DataContext _context = context;

    public async Task<Person?> GetPerson_repo(string codiceFiscale) {
        try
        {
            return await _context.People.FirstOrDefaultAsync(e => e.CodiceFiscale.Equals(codiceFiscale, StringComparison.CurrentCultureIgnoreCase));
        } catch (Exception) { throw; }
    }
    
    public async Task<IEnumerable<Person>> GetAllPerson_repo() {
        try
        {
            return await _context.People.ToListAsync();
        } catch (Exception) { throw; }
    }
    
    public async Task<int> PostPerson_repo() {
        try
        {
            return -1;
        } catch (Exception) { throw; }
    }
    
    public async Task<int> PutPerson_repo() {
        try
        {
            return -1;
        } catch (Exception) { throw; }
    }
    
    public async Task<int> DeletePerson_repo() {
        try
        {
            return -1;
        } catch (Exception) { throw; }
    }
}