using Microsoft.EntityFrameworkCore;
using system.core._.Domain.Entity.User;

namespace system.api._.Data;

class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    DbSet<Person> People => Set<Person>();
    DbSet<Client> Clients => Set<Client>();
    DbSet<Employee> Employees => Set<Employee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("persone");
            entity.HasKey(pk => pk.Id);
          
            entity.Property(e => e.Id).HasColumnOrder(1).HasColumnName("id").HasColumnType("int");
            entity.Property(e => e.CodiceFiscale).HasColumnOrder(2).HasColumnName("codice_fiscale").HasColumnType("varchar(16)");
            entity.Property(e => e.Nome).HasColumnOrder(3).HasColumnName("nome").HasColumnType("varchar(100)");
            entity.Property(e => e.Cognome).HasColumnOrder(4).HasColumnName("cognome").HasColumnType("varchar(100)");
            entity.Property(e => e.Sesso).HasColumnOrder(5).HasColumnName("sesso").HasColumnType("varchar(1)");
            entity.Property(e => e.DataNascita).HasColumnOrder(6).HasColumnName("data_nascita").HasColumnType("date");
            entity.Property(e => e.Citta).HasColumnOrder(7).HasColumnName("citta").HasColumnType("varchar(100)");
            entity.Property(e => e.Provincia).HasColumnOrder(8).HasColumnName("provincia").HasColumnType("varchar(5)");
            entity.Property(e => e.CodicePostale).HasColumnOrder(9).HasColumnName("codice_postale").HasColumnType("varchar(10)");
            entity.Property(e => e.Indirizzo).HasColumnOrder(10).HasColumnName("indirizzo").HasColumnType("varchar(255)");
            entity.Property(e => e.NumeroContatto).HasColumnOrder(11).HasColumnName("numero_contatto").HasColumnType("varchar(20)");
            entity.Property(e => e.Email).HasColumnOrder(12).HasColumnName("email").HasColumnType("varchar(100)");
            entity.Property(e => e.IsDeleted).HasColumnOrder(13).HasColumnName("is_deleted").HasColumnType("tinyint(1)");
            entity.Property(e => e.CreatedAt).HasColumnOrder(14).HasColumnName("created_at").HasColumnType("datetime(6)");
            entity.Property(e => e.ModifiedAt).HasColumnOrder(15).HasColumnName("modified_at").HasColumnType("datetime(6)");
        });
        
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("clienti");
            entity.HasKey(pk => pk.Id);
            entity.HasOne(np => np.Person).WithOne().HasForeignKey<Client>(fk => fk.PersonId);

            entity.Property(e => e.Id).HasColumnOrder(1).HasColumnName("id").HasColumnType("int");
            entity.Property(e => e.PersonId).HasColumnOrder(2).HasColumnName("codice_fiscale").HasColumnType("varchar(16)");
            entity.Property(e => e.CodiceCliente).HasColumnOrder(3).HasColumnName("codice_cliente").HasColumnType("varchar(20)");
            entity.Property(e => e.IsFidelizzato).HasColumnOrder(4).HasColumnName("is_fidelizzato").HasColumnType("tinyint(1)");
            entity.Property(e => e.IsIscrittoNewsletter).HasColumnOrder(5).HasColumnName("is_iscritto_newsletter").HasColumnType("tinyint(1)");
            entity.Property(e => e.IsDeleted).HasColumnOrder(13).HasColumnName("is_deleted").HasColumnType("tinyint(1)");
            entity.Property(e => e.CreatedAt).HasColumnOrder(14).HasColumnName("created_at").HasColumnType("datetime(6)");
            entity.Property(e => e.ModifiedAt).HasColumnOrder(15).HasColumnName("modified_at").HasColumnType("datetime(6)");
        });
        
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("dipendenti");
            entity.HasKey(pk => pk.Id);
            entity.HasOne(np => np.Person).WithOne().HasForeignKey<Employee>(fk => fk.PersonId);

            entity.Property(e => e.Id).HasColumnOrder(1).HasColumnName("id").HasColumnType("int");
            entity.Property(e => e.PersonId).HasColumnOrder(2).HasColumnName("codice_fiscale").HasColumnType("varchar(16)");
            entity.Property(e => e.CodiceMeccanografico).HasColumnOrder(3).HasColumnName("codice_meccanografico").HasColumnType("varchar(10)");
            entity.Property(e => e.Ruolo).HasColumnOrder(4).HasColumnName("ruolo").HasColumnType("varchar(50)");
            entity.Property(e => e.Salario).HasColumnOrder(5).HasColumnName("salario").HasColumnType("decimal(18,2)");
            entity.Property(e => e.IsDeleted).HasColumnOrder(13).HasColumnName("is_deleted").HasColumnType("tinyint(1)");
            entity.Property(e => e.CreatedAt).HasColumnOrder(14).HasColumnName("created_at").HasColumnType("datetime(6)");
            entity.Property(e => e.ModifiedAt).HasColumnOrder(15).HasColumnName("modified_at").HasColumnType("datetime(6)");
        });
    }
}