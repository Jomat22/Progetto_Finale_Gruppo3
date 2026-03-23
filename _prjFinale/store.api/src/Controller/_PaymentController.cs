using Microsoft.AspNetCore.Mvc;
using store.core.src.Interface;
namespace store.api.src.Controller;

[ApiController]
[Route("api/[controller]")]
public class _PaymentController : ControllerBase
{
    private readonly IPaymentContext _paymentContext;

    public _PaymentController(IPaymentContext paymentContext)
    {
        _paymentContext = paymentContext;
    }

    [HttpPost("pay")]
    public IActionResult Pay(string provider, decimal amount)
    {
        var result = _paymentContext.ExecuteStrategy(provider, amount);
        
        if (result.StartsWith("Errore"))
            return BadRequest(result);

        return Ok(result);
    }
}

/* 
    public async Task<Receipt> CreateReceiptAsync(ScontrinoCreateRequest dto)
{
    // Creiamo la testata
    var nuovoScontrino = new Receipt
    {
        DataEmissione = DateTime.Now,
        MetodoPagamento = dto.MetodoPagamento,
        CreatedAt = DateTime.Now,
        ModifiedAt = DateTime.Now,
        Dettagli = new List<ReceiptDetail>() // Lista vuota pronta ad essere popolata
    };

    decimal calcoloTotaleScontrino = 0;

    foreach (var item in dto.Prodotti)
    {
        // 1. Cerchiamo il prodotto per avere il prezzo reale dal DB (sicurezza!)
        var prodottoDb = await _context.Products.FindAsync(item.ProdottoId);
        if (prodottoDb == null) throw new Exception($"Prodotto {item.ProdottoId} non trovato.");

        // 2. Controllo disponibilità magazzino
        if (prodottoDb.Qnt < item.Quantita) throw new Exception($"Stock insufficiente per {prodottoDb.Nome}.");

        // 3. Creiamo la riga di dettaglio
        var riga = new ReceiptDetail
        {
            ProdottoId = item.ProdottoId,
            Quantita = item.Quantita,
            PrezzoTotale = prodottoDb.Prezzo * item.Quantita, // Calcolato qui!
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now
        };

        // 4. Aggiorniamo i parziali
        calcoloTotaleScontrino += riga.PrezzoTotale;
        
        // 5. Aggiorniamo
        prodottoDb.Qnt -= item.Quantita;

        // 6. Aggiungo alla lista (Qui scatta il legame automatico)
        nuovoScontrino.Dettagli.Add(riga);
    }

    nuovoScontrino.TotaleDefinitivo = calcoloTotaleScontrino;

    // Salvataggio finale
    _context.Receipts.Add(nuovoScontrino);
    await _context.SaveChangesAsync(); 
    // Qui EF Core: 
    // 1. Inserisce Receipt 
    // 2. Legge l'ID generato 
    // 3. Lo mette in tutti i ReceiptDetail 
    // 4. Inserisce i dettagli

    return nuovoScontrino;
}
 */