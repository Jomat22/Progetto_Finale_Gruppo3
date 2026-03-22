using Microsoft.EntityFrameworkCore;
using store.api.src.Data;
using store.core.src.Domain.Entity.User;
namespace store.api.src.Infrastructure.Repo;

public class ClientRepository(DataContext context)
{
    private readonly DataContext _context = context;

    // Per verificare se id delle persona è già in utilizzo
    public async Task<Client?> GetClientByPersonId_repo(int personId, bool asNoTracking = false) 
    {
        try
        {
            IQueryable<Client> query = _context.Clients.AsQueryable();
            if (asNoTracking) { query.AsNoTracking(); }

            return await query.FirstOrDefaultAsync(e => e.PersonId == personId);
        } catch (Exception) { throw; }
    }

    public async Task<Client?> GetClientByCCode_repo(string codiceCliente, bool asNoTracking = false) 
    {
        try
        {
            IQueryable<Client> query = _context.Clients.AsQueryable();
            if (asNoTracking) { query.AsNoTracking(); }

            return await query.FirstOrDefaultAsync(e => e.CodiceCliente.ToLower() == codiceCliente.ToLower());
        } catch (Exception) { throw; }
    }
    
    public async Task<IEnumerable<Client>> GetAllClient_repo(bool asNoTracking = true)
    {
        try 
        {
            IQueryable<Client> query = _context.Clients.AsQueryable();
            if (asNoTracking) { query = query.AsNoTracking(); }

            return await query.ToListAsync();
        } catch(Exception) { throw; }
    }
    
    public async Task<int> PostClient_repo(Client client) 
    {
        try
        {
            _context.Clients.Add(client);
            return await _context.SaveChangesAsync();
        } catch (Exception) { throw; }
    }
    
    public async Task<int> PutClient_repo() 
    {
        try
        {
            return await _context.SaveChangesAsync();
        } catch (Exception) { throw; }
    }
    
    public async Task<int> DeleteClient_repo(Client client) {
        try
        {
            _context.Remove(client);
            return await _context.SaveChangesAsync();
        } catch (Exception) { throw; }
    }
}