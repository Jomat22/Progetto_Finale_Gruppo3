using store.core._.Domain.Interface;
namespace store.core._.Domain.Entity.Catalog;

public class Product : BaseEntity, IProduct
{
    // Stock Keeping Unit (scelto perché standard universale per magazzini/e-commerce)
    public string Sku { get; set; } = default!;
    public string Nome { get; set; } = default!;
    public int Qnt { get; set; }
    
    public Product() : base() {}
    public Product(int id, bool isDeleted, DateTime createdAt, DateTime modifiedAt, 
        string sku, string nome, int qnt) : base(id, isDeleted, createdAt, modifiedAt)
    {
        Sku = sku;
        Nome = nome;
        Qnt = qnt;
    }

    public string Descrizione() => Nome;
}