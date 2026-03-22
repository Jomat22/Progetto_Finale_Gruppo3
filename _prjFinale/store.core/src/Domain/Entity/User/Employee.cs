namespace store.core.src.Domain.Entity.User;

public class Employee : BaseEntity
{
    public int PersonId { get; set; }
    public string CodiceMeccanografico { get; set; } = default!;
    public string EmailAziendale { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Ruolo { get; set; } = default!;
    public decimal Salario { get; set; }

    // Proprietà navigazione
    public Person? Person { get; set; }

    public Employee() : base() { }
    public Employee(int id, bool isDeleted, DateTime createdAt, DateTime modifiedAt, int personId, 
        string codiceMeccanografico, string emailAziendale, string password, string ruolo, decimal salario) : base(id, isDeleted, createdAt, modifiedAt) 
    { 
        PersonId = personId; 
        CodiceMeccanografico = codiceMeccanografico;
        EmailAziendale = emailAziendale;
        Password = password;
        Ruolo = ruolo;
        Salario = salario;
    }
}