using system.core._.Interface;
namespace system.core._.Domain.Entity.Catalog;

public class Product : BaseEntity, IProduct
{
    // Stock Keeping Unit (scelto perché standard universali per magazzini/e-commerce)
    public string Sku { get; set; }
    public string Name { get; set; }
    public int Qnt { get; set; }
    public Product(int id, bool isActive, DateTime createdAt, DateTime modifiedAt, 
        string sku, string name, int qnt) : base(id, isActive, createdAt, modifiedAt)
    {
        Sku = sku;
        Name = name;
        Qnt = qnt;
    }

    public string Descrizione() => Name;
}