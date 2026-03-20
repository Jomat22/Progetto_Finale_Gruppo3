class Employee : BaseEntity
{
    public int PersonId { get; set; }
    public string CodiceMeccanografico { get; set; } = default!;
    public string Ruolo { get; set; } = default!;
    public decimal Salario { get; set; }

    // Proprietà navigazione
    public Person? person { get; set; }

    public Employee(int id, bool isDeleted, DateTime createdAt, DateTime modifiedAt, int personId, 
        string codiceMeccanografico, string ruolo, decimal salario) : base(id, isDeleted, createdAt, modifiedAt) 
    { 
        PersonId = personId; 
        CodiceMeccanografico = codiceMeccanografico;
        Ruolo = ruolo;
        Salario = salario;
    }
}