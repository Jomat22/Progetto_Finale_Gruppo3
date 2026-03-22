namespace store.core.src.Domain.Entity.User;

public class Person : BaseEntity
{
    public string CodiceFiscale { get; set; } = default!; 
    public string Nome { get; set; } = default!; 
    public string Cognome { get; set; } = default!; 
    public string Sesso { get; set; } = default!; 
    public DateOnly DataNascita { get; set; }
    public string Citta { get; set; } = default!;
    public string Provincia { get; set; } = default!;
    public string CodicePostale { get; set; } = default!;
    public string Indirizzo { get; set; } = default!;
    public string NumeroContatto { get; set; } = default!;
    public string Email { get; set; } = default!;

    // Da 'private' a 'protected' per permettere al 'DbContext' di funzionare correttamente, adesso su 'public' per il seeding (.HasData)
    // N.B.: Si poteva usare il costruttore parametrizzato per passare direttamente gli argomenti, ho preferito invece
    // usare il primo per specificare manualmente Proprietà e valore da asseganre nel 'DbContext'.
    public Person() : base() { }
    public Person(int id, bool isDeleted, DateTime createdAt, DateTime modifiedAt, string codiceFiscale, string nome, 
        string cognome, string sesso, DateOnly dataNascita, string citta, string provincia, string codicePostale, 
        string indirizzo, string numeroContatto, string email) : base(id, isDeleted, createdAt, modifiedAt)
    {
        CodiceFiscale = codiceFiscale; 
        Nome = nome; 
        Cognome = cognome; 
        Sesso = sesso; 
        DataNascita = dataNascita; 
        Citta = citta; 
        Provincia = provincia; 
        CodicePostale = codicePostale; 
        Indirizzo = indirizzo; 
        NumeroContatto = numeroContatto; 
        Email = email;
    }
}