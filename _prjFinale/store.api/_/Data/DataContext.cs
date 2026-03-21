using Microsoft.EntityFrameworkCore;
using store.core._.Domain.Entity.User;
namespace store.api._.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
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
            entity.HasIndex(e => e.CodiceFiscale).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();

            entity.Property(e => e.Id).HasColumnOrder(1).HasColumnName("id").HasColumnType("int").ValueGeneratedOnAdd().IsRequired();
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
            entity.Property(e => e.Email).HasColumnOrder(12).HasColumnName("email").HasColumnType("varchar(255)");
            entity.Property(e => e.IsDeleted).HasColumnOrder(13).HasColumnName("is_deleted").HasColumnType("tinyint(1)");
            entity.Property(e => e.CreatedAt).HasColumnOrder(14).HasColumnName("created_at").HasColumnType("datetime(6)");
            entity.Property(e => e.ModifiedAt).HasColumnOrder(15).HasColumnName("modified_at").HasColumnType("datetime(6)");
        });
        
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("clienti");
            entity.HasKey(pk => pk.Id);
            entity.HasOne(np => np.Person).WithOne().HasForeignKey<Client>(fk => fk.PersonId);
            entity.HasIndex(e => e.CodiceCliente).IsUnique();

            entity.Property(e => e.Id).HasColumnOrder(1).HasColumnName("id").HasColumnType("int").ValueGeneratedOnAdd().IsRequired();
            entity.Property(e => e.PersonId).HasColumnOrder(2).HasColumnName("person_id").HasColumnType("int").IsRequired();;
            entity.Property(e => e.CodiceCliente).HasColumnOrder(3).HasColumnName("codice_cliente").HasColumnType("varchar(20)");
            entity.Property(e => e.IsFidelizzato).HasColumnOrder(4).HasColumnName("is_fidelizzato").HasColumnType("tinyint(1)");
            entity.Property(e => e.IsIscrittoNewsletter).HasColumnOrder(5).HasColumnName("is_iscritto_newsletter").HasColumnType("tinyint(1)");
            entity.Property(e => e.IsDeleted).HasColumnOrder(6).HasColumnName("is_deleted").HasColumnType("tinyint(1)");
            entity.Property(e => e.CreatedAt).HasColumnOrder(7).HasColumnName("created_at").HasColumnType("datetime(6)");
            entity.Property(e => e.ModifiedAt).HasColumnOrder(8).HasColumnName("modified_at").HasColumnType("datetime(6)");
        });
        
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("dipendenti");
            entity.HasKey(pk => pk.Id);
            entity.HasOne(np => np.Person).WithOne().HasForeignKey<Employee>(fk => fk.PersonId);
            entity.HasIndex(e => e.CodiceMeccanografico).IsUnique();

            entity.Property(e => e.Id).HasColumnOrder(1).HasColumnName("id").HasColumnType("int").ValueGeneratedOnAdd().IsRequired();
            entity.Property(e => e.PersonId).HasColumnOrder(2).HasColumnName("person_id").HasColumnType("int").IsRequired();;
            entity.Property(e => e.CodiceMeccanografico).HasColumnOrder(3).HasColumnName("codice_meccanografico").HasColumnType("varchar(10)");
            entity.Property(e => e.Password).HasColumnOrder(4).HasColumnName("password").HasColumnType("varchar(24)");
            entity.Property(e => e.Ruolo).HasColumnOrder(5).HasColumnName("ruolo").HasColumnType("varchar(50)");
            entity.Property(e => e.Salario).HasColumnOrder(6).HasColumnName("salario").HasColumnType("decimal(18,2)");
            entity.Property(e => e.IsDeleted).HasColumnOrder(7).HasColumnName("is_deleted").HasColumnType("tinyint(1)");
            entity.Property(e => e.CreatedAt).HasColumnOrder(8).HasColumnName("created_at").HasColumnType("datetime(6)");
            entity.Property(e => e.ModifiedAt).HasColumnOrder(9).HasColumnName("modified_at").HasColumnType("datetime(6)");
        });
    }
}