namespace store.core._.Domain.Entity.User;

public class Client : BaseEntity
{
    public int PersonId { get; set; }
    public string CodiceCliente { get; set; } = default!;
    public bool IsFidelizzato { get; set; }
    public bool IsIscrittoNewsletter { get; set; }

    // Proprietà navigazione
    public Person? Person { get; set; }

    public Client() : base() { }
    public Client(int id, bool isDeleted, DateTime createdAt, DateTime modifiedAt, 
        int personId, string codiceCliente, bool isFidelizzato) : base(id, isDeleted, createdAt, modifiedAt) 
    { 
        PersonId = personId;
        CodiceCliente = codiceCliente;
        IsFidelizzato = isFidelizzato; 
    }
}