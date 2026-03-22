using store.core.src.Interface;
namespace store.core.src.Domain.Entity.Catalog;

public class Product : BaseEntity, IProduct
{
    // Stock Keeping Unit (scelto perché standard universale per magazzini/e-commerce)
    public string Sku { get; set; } = default!;
    public string Nome { get; set; } = default!;
    public decimal Prezzo { get; set; }
    public int Quantita { get; set; }
    
    public Product() : base() {}
    public Product(int id, bool isDeleted, DateTime createdAt, DateTime modifiedAt, 
        string sku, string nome, decimal prezzo, int quantita) : base(id, isDeleted, createdAt, modifiedAt)
    {
        Sku = sku;
        Nome = nome;
        Prezzo = prezzo;
        Quantita = quantita;
    }

    public string Descrizione() => Nome;
}