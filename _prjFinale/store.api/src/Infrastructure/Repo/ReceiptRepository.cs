using Microsoft.EntityFrameworkCore;
using store.api.src.Data;
using store.core.src.Domain.Entity.Sales;
namespace store.api.src.Infrastructure.Repo;

public class ReceiptRepository(DataContext context)
{
    private readonly DataContext _context = context;

    // Per verificare se l'id delle ricevute è già in utilizzo
    public async Task<Receipt?> GetReceiptById_repo(int id, bool asNoTracking = true)
    {
        try
        {
            IQueryable<Receipt> query = _context.Receipts.Include(r => r.RicevutaDettagli).ThenInclude(d => d.Prodotto);
            if (asNoTracking) { query = query.AsNoTracking(); }

            return await query.FirstOrDefaultAsync(r => r.Id == id);
        }
        catch (Exception) { throw; }
    }
    public async Task<IEnumerable<Receipt>> GetAllReceipt_repo(bool asNoTracking = true)
    {
        try
        {
            IQueryable<Receipt> query = _context.Receipts.Include(r => r.RicevutaDettagli).ThenInclude(d => d.Prodotto);
            if (asNoTracking) {query = query.AsNoTracking(); }

            return await query.OrderByDescending(r => r.DataEmissione).ToListAsync();
        }
        catch (Exception) { throw; }
    }


    public async Task<IEnumerable<Receipt>> GetReceiptByMetodo_repo(string metodoPagamento, bool asNoTracking = true)
    {
        try
        {
            IQueryable<Receipt> query = _context.Receipts.Include(r => r.RicevutaDettagli).Where(r => r.MetodoPagamento.ToLower() == metodoPagamento);
            if (asNoTracking) { query = query.AsNoTracking(); }

            return await query.OrderByDescending(r => r.DataEmissione).ToListAsync();
        }
        catch (Exception) { throw; }
    }

    public async Task<IEnumerable<Receipt>> GetReceiptToday_repo(bool asNoTracking = true)
    {
        try
        {
            IQueryable<Receipt> query = _context.Receipts.Include(r => r.RicevutaDettagli).Where(r => r.DataEmissione.Date == DateTime.UtcNow.Date);
            if (asNoTracking) { query = query.AsNoTracking(); }

            return await query.OrderByDescending(r => r.DataEmissione).ToListAsync();
        }
        catch (Exception) { throw; }
    }

    public async Task<int> PostReceipt_repo(Receipt receipt)
    {
        try
        {
            _context.Receipts.Add(receipt);
            return await _context.SaveChangesAsync();
        }
        catch (Exception) { throw; }
    }

    public async Task<int> DeleteReceipt_repo(Receipt receipt)
    {
        try
        {
            _context.Remove(receipt);
            return await _context.SaveChangesAsync();
        }
        catch (Exception) { throw; }
    }
}