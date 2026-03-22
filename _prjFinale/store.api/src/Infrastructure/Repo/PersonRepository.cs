using Microsoft.EntityFrameworkCore;
using store.api.src.Data;
using store.core.src.Domain.Entity.User;
namespace store.api.src.Infrastructure.Repo;

public class PersonRepository(DataContext context)
{
    private readonly DataContext _context = context;

    public async Task<Person?> GetPersonById_repo(int id, bool asNoTracking = true) 
    {
        try
        {
            IQueryable<Person> query = _context.People.AsQueryable();
            if (asNoTracking) { query.AsNoTracking(); }

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        } catch (Exception) { throw; }
    }

    public async Task<Person?> GetPersonByTaxCode_repo(string codiceFiscale, bool asNoTracking = true) 
    {
        try
        {
            IQueryable<Person> query = _context.People.AsQueryable();
            if (asNoTracking) { query.AsNoTracking(); }

            return await query.FirstOrDefaultAsync(e => e.CodiceFiscale.ToLower() == codiceFiscale.ToLower());
        } catch (Exception) { throw; }
    }
    
    public async Task<IEnumerable<Person>> GetAllPerson_repo(bool asNoTracking = true) // 'true' ↓
            /*  Per non dimenticare...
                Dev 1: "Oddio, la query è lentissima... il server sta ESPLODENDO!"
                Dev 2: "STACCAH! STACCAH TUTTOH! CI STANNO TRACCIANDO! EF CORE CI STA TRACCIANDO OGNI SINGOLA PROPERTY!"
                Dev 1: "Ma io volevo solo fare una lista... non volevo salvarle!"
                Dev 2: "NON IMPORTA! IL CHANGE TRACKER È GIÀ PARTITOH! STACCA IL DATACONTEXT! STACCA LA CONNESSIONE AL DB! CI STANNO TRACCIANDO, CHIUDI TUTTOOOOH!" 
            */
    {
        try 
        {
            IQueryable<Person> query = _context.People.AsQueryable();
            if (asNoTracking) { query = query.AsNoTracking(); }

            return await query.ToListAsync();
        } catch(Exception) { throw; }
    }
    
    public async Task<int> PostPerson_repo(Person person) 
    {
        try
        {
            _context.People.Add(person);
            return await _context.SaveChangesAsync();
        } catch (Exception) { throw; }
    }
    
    public async Task<int> PutPerson_repo() 
    {
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