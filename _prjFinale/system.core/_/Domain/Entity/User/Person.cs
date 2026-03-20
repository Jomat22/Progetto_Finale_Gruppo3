namespace system.core._.Domain.Entity.User;

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

    public Person(int id, bool isActive, DateTime createdAt, DateTime modifiedAt, string codiceFiscale, string nome, 
        string cognome, string sesso, DateOnly dataNascita, string citta, string provincia, string codicePostale, string indirizzo) : base(id, isActive, createdAt, modifiedAt)
    {
        CodiceFiscale = codiceFiscale; Nome = nome; Cognome = cognome; Sesso = sesso; DataNascita = dataNascita;
        Citta = citta; Provincia = provincia; CodicePostale = codicePostale; Indirizzo = indirizzo;
    }
}