using store.core._.Interface;
namespace store.core._.Domain.Entity.Catalog;

public class Product : BaseEntity, IProduct
{
    // Stock Keeping Unit (scelto perché standard universale per magazzini/e-commerce)
    public string Sku { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Qnt { get; set; }
    
    protected Product() {}
    public Product(int id, bool isActive, DateTime createdAt, DateTime modifiedAt, 
        string sku, string name, int qnt) : base(id, isActive, createdAt, modifiedAt)
    {
        Sku = sku;
        Name = name;
        Qnt = qnt;
    }

    public string Descrizione() => Name;
}