using Microsoft.EntityFrameworkCore;
using store.core.src.Domain.Entity.Catalog;
using store.core.src.Domain.Entity.Sales;
using store.core.src.Domain.Entity.User;
namespace store.api.src.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Person> People => Set<Person>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Receipt> Receipts => Set<Receipt>();
    public DbSet<ReceiptDetail> ReceiptDetails => Set<ReceiptDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("persone");
            entity.HasKey(pk => pk.Id);
            entity.HasIndex(e => e.CodiceFiscale).IsUnique();

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

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("dipendenti");
            entity.HasKey(pk => pk.Id);
            entity.HasOne(np => np.Person).WithOne().HasForeignKey<Employee>(fk => fk.PersonId).OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(e => e.CodiceMeccanografico).IsUnique();
            entity.HasIndex(e => e.EmailAziendale).IsUnique();

            entity.Property(e => e.Id).HasColumnOrder(1).HasColumnName("id").HasColumnType("int").ValueGeneratedOnAdd().IsRequired();
            entity.Property(e => e.PersonId).HasColumnOrder(2).HasColumnName("person_id").HasColumnType("int").IsRequired();
            entity.Property(e => e.CodiceMeccanografico).HasColumnOrder(3).HasColumnName("codice_meccanografico").HasColumnType("varchar(20)");
            entity.Property(e => e.EmailAziendale).HasColumnOrder(4).HasColumnName("email_aziendale").HasColumnType("varchar(255)");
            entity.Property(e => e.Password).HasColumnOrder(5).HasColumnName("password").HasColumnType("varchar(24)");
            entity.Property(e => e.Ruolo).HasColumnOrder(6).HasColumnName("ruolo").HasColumnType("varchar(50)");
            entity.Property(e => e.Salario).HasColumnOrder(7).HasColumnName("salario").HasColumnType("decimal(18,2)");
            entity.Property(e => e.IsDeleted).HasColumnOrder(8).HasColumnName("is_deleted").HasColumnType("tinyint(1)");
            entity.Property(e => e.CreatedAt).HasColumnOrder(9).HasColumnName("created_at").HasColumnType("datetime(6)");
            entity.Property(e => e.ModifiedAt).HasColumnOrder(10).HasColumnName("modified_at").HasColumnType("datetime(6)");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("clienti");
            entity.HasKey(pk => pk.Id);
            entity.HasOne(np => np.Person).WithOne().HasForeignKey<Client>(fk => fk.PersonId).OnDelete(DeleteBehavior.Cascade); ;
            entity.HasIndex(e => e.CodiceCliente).IsUnique();

            entity.Property(e => e.Id).HasColumnOrder(1).HasColumnName("id").HasColumnType("int").ValueGeneratedOnAdd().IsRequired();
            entity.Property(e => e.PersonId).HasColumnOrder(2).HasColumnName("person_id").HasColumnType("int").IsRequired();
            entity.Property(e => e.CodiceCliente).HasColumnOrder(3).HasColumnName("codice_cliente").HasColumnType("varchar(20)");
            entity.Property(e => e.IsFidelizzato).HasColumnOrder(4).HasColumnName("is_fidelizzato").HasColumnType("tinyint(1)");
            entity.Property(e => e.IsIscrittoNewsletter).HasColumnOrder(5).HasColumnName("is_iscritto_newsletter").HasColumnType("tinyint(1)");
            entity.Property(e => e.IsDeleted).HasColumnOrder(6).HasColumnName("is_deleted").HasColumnType("tinyint(1)");
            entity.Property(e => e.CreatedAt).HasColumnOrder(7).HasColumnName("created_at").HasColumnType("datetime(6)");
            entity.Property(e => e.ModifiedAt).HasColumnOrder(8).HasColumnName("modified_at").HasColumnType("datetime(6)");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("prodotti");
            entity.HasKey(pk => pk.Id);
            entity.HasIndex(e => e.Sku).IsUnique();

            entity.Property(e => e.Id).HasColumnOrder(1).HasColumnName("id").HasColumnType("int").ValueGeneratedOnAdd().IsRequired();
            entity.Property(e => e.Sku).HasColumnOrder(2).HasColumnName("sku").HasColumnType("varchar(20)").IsRequired();
            entity.Property(e => e.Nome).HasColumnOrder(3).HasColumnName("nome").HasColumnType("varchar(100)");
            entity.Property(e => e.Prezzo).HasColumnOrder(4).HasColumnName("prezzo").HasColumnType("decimal(10,2)");
            entity.Property(e => e.Quantita).HasColumnOrder(5).HasColumnName("quantita").HasColumnType("int");
            entity.Property(e => e.IsDeleted).HasColumnOrder(6).HasColumnName("is_deleted").HasColumnType("tinyint(1)");
            entity.Property(e => e.CreatedAt).HasColumnOrder(7).HasColumnName("created_at").HasColumnType("datetime(6)");
            entity.Property(e => e.ModifiedAt).HasColumnOrder(8).HasColumnName("modified_at").HasColumnType("datetime(6)");
        });

        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.ToTable("scontrini");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ClientId).HasColumnOrder(5).HasColumnName("client_id").HasColumnType("int").IsRequired();
            entity.HasOne(r => r.Client).WithMany().HasForeignKey(r => r.ClientId).OnDelete(DeleteBehavior.Restrict);
            entity.Property(e => e.Id).HasColumnOrder(1).HasColumnName("id").HasColumnType("int").ValueGeneratedOnAdd().IsRequired();
            entity.Property(e => e.DataEmissione).HasColumnOrder(2).HasColumnName("data_emissione").HasColumnType("datetime(6)").IsRequired();
            entity.Property(e => e.TotaleDefinitivo).HasColumnOrder(3).HasColumnName("totale_definitivo").HasColumnType("decimal(10,2)").IsRequired();
            entity.Property(e => e.MetodoPagamento).HasColumnOrder(4).HasColumnName("metodo_pagamento").HasColumnType("varchar(50)");
            entity.Property(e => e.IsDeleted).HasColumnOrder(6).HasColumnName("is_deleted").HasColumnType("tinyint(1)");
            entity.Property(e => e.CreatedAt).HasColumnOrder(7).HasColumnName("created_at").HasColumnType("datetime(6)");
            entity.Property(e => e.ModifiedAt).HasColumnOrder(8).HasColumnName("modified_at").HasColumnType("datetime(6)");
        });

        modelBuilder.Entity<ReceiptDetail>(entity =>
        {
            entity.ToTable("scontrini_dettagli");
            entity.HasKey(e => e.Id);
            entity.HasOne(np => np.Ricevuta).WithMany(np => np.RicevutaDettagli).HasForeignKey(fk => fk.RicevutaId).OnDelete(DeleteBehavior.Cascade); // Se elimini lo scontrino, elimina i dettagli
            entity.HasOne(np => np.Prodotto).WithMany().HasForeignKey(fk => fk.ProdottoId);

            entity.Property(e => e.Id).HasColumnOrder(1).HasColumnName("id").HasColumnType("int").ValueGeneratedOnAdd().IsRequired();
            entity.Property(e => e.RicevutaId).HasColumnOrder(2).HasColumnName("ricevuta_id").HasColumnType("int").IsRequired();
            entity.Property(e => e.ProdottoId).HasColumnOrder(3).HasColumnName("prodotto_id").HasColumnType("int").IsRequired();
            entity.Property(e => e.Quantita).HasColumnOrder(4).HasColumnName("quantita").HasColumnType("int");
            entity.Property(e => e.PrezzoTotale).HasColumnOrder(5).HasColumnName("prezzo_totale").HasColumnType("decimal(10,2)");
            entity.Property(e => e.IsDeleted).HasColumnOrder(6).HasColumnName("is_deleted").HasColumnType("tinyint(1)");
            entity.Property(e => e.CreatedAt).HasColumnOrder(7).HasColumnName("created_at").HasColumnType("datetime(6)");
            entity.Property(e => e.ModifiedAt).HasColumnOrder(8).HasColumnName("modified_at").HasColumnType("datetime(6)");
        });

        // ====================================================================================================
        // 1. SEED PERSONE (30 RECORD)
        /*
            * Nota 1: Le persone da 21 a 30 sono "scollegate" ai fini di test (assegnazione manuale id persona).
            * Nota 2: "The model for context 'DataContext' has pending changes" se si utilizza ad esempio 'DateTime.UtcNow' per
            * le date, il  valore cambia ogni volta, 'EF' avvisa che per renderli effettivi, necessita di una nuova migrazione. 
            * Pertanto userò valori statici (deterministici) cosicché 'EF', analizzando il modello per le migration e 
            * confrontando i valori del seed, non vedrà variazioni durante l'esecuzione (non ci sono cambiamenti, i seed sono gli stessi).
        */
        var persone = new List<Person>();
        for (int i = 1; i <= 30; i++)
        {
            persone.Add(new Person
            {
                Id = i,
                CodiceFiscale = $"{i:D16}", // Es: ...001, ...002, ecc..
                Nome = $"Nome{i}",
                Cognome = $"Cognome{i}",
                Sesso = i % 2 == 0 ? "M" : "F",
                DataNascita = new DateOnly(1979, 12, 1).AddMonths(i),
                Citta = "Roma",
                Provincia = "RM",
                CodicePostale = "00100",
                Indirizzo = $"Via delle Prove {i}",
                NumeroContatto = $"33300000{i:D2}",
                Email = $"persona{i}@example.com",
                IsDeleted = false,
                CreatedAt = default,
                ModifiedAt = default
            });
        }
        modelBuilder.Entity<Person>().HasData(persone);

        // 2. SEED DIPENDENTI (10 RECORD - Collegati a Person 1-10)
        var dipendenti = new List<Employee>();
        for (int i = 1; i <= 10; i++)
        {
            dipendenti.Add(new Employee
            {
                Id = i,
                PersonId = i, // FK verso Person 1-10
                CodiceMeccanografico = $"{i:D20}",
                EmailAziendale = $"email{i}@gmail.com",
                Password = "Password123!",
                Ruolo = i % 2 == 0 ? "Admin" : "User",
                Salario = 1500.00m + (i * 100),
                IsDeleted = false,
                CreatedAt = default,
                ModifiedAt = default
            });
        }
        modelBuilder.Entity<Employee>().HasData(dipendenti);

        // 3. SEED CLIENTI (10 RECORD - Collegati a Person 11-20)
        var clienti = new List<Client>();
        for (int i = 1; i <= 10; i++)
        {
            int personId = i + 10; // FK verso Person 11-20
            clienti.Add(new Client
            {
                Id = i,
                PersonId = personId,
                CodiceCliente = $"{i:D20}",
                IsFidelizzato = i % 2 == 0,
                IsIscrittoNewsletter = true,
                IsDeleted = false,
                CreatedAt = default,
                ModifiedAt = default
            });
        }
        modelBuilder.Entity<Client>().HasData(clienti);

        // 4. SEED PRODOTTI (20 RECORD)
        var prodotti = new List<Product>();
        for (int i = 1; i <= 20; i++)
        {
            prodotti.Add(new Product
            {
                Id = i,
                Sku = $"{i:D20}",
                Nome = $"Prodotto Esempio {i}",
                Prezzo = i,
                Quantita = i * 10, // Quantità: 10, 20, 30...
                IsDeleted = false,
                CreatedAt = default,
                ModifiedAt = default
            });
        }
        modelBuilder.Entity<Product>().HasData(prodotti);

        // 5. SEED SCONTRINI (20 RECORD)
        var scontrini = new List<Receipt>();
        for (int i = 1; i <= 20; i++)
        {
            scontrini.Add(new Receipt
            {
                Id = i,
                ClientId = (i % 10) + 1,
                DataEmissione = new DateTime(2024, 1, 1).AddDays(i), // Date scalate nel 2024
                MetodoPagamento = i % 2 == 0 ? "Contanti" : "Carta di Credito",
                TotaleDefinitivo = 0, // Lo lasceremo a 0 o calcolato, ma nel Seed serve un valore
                IsDeleted = false,
                CreatedAt = new DateTime(2024, 1, 1),
                ModifiedAt = new DateTime(2024, 1, 1)
            });
        }
        modelBuilder.Entity<Receipt>().HasData(scontrini);

        // 6. SEED SCONTRINI DETTAGLI (Esempio: 2 prodotti per ogni scontrino)
        var dettagli = new List<ReceiptDetail>();
        int dettaglioIdCounter = 1;

        for (int i = 1; i <= 20; i++)
        {
            // Aggiungiamo il primo prodotto alla ricevuta
            dettagli.Add(new ReceiptDetail
            {
                Id = dettaglioIdCounter++,
                RicevutaId = i,      // FK verso lo scontrino corrente
                ProdottoId = i,      // Usa il prodotto con ID uguale allo scontrino
                Quantita = 1,
                PrezzoTotale = i, // Prezzo fittizio basato sull'ID del prodotto
                IsDeleted = false,
                CreatedAt = new DateTime(2024, 1, 1),
                ModifiedAt = new DateTime(2024, 1, 1)
            });

            // Aggiungiamo un secondo prodotto alla stessa ricevuta (es: il prodotto successivo)
            int secondoProdottoId = (i < 20) ? i + 1 : 1;
            dettagli.Add(new ReceiptDetail
            {
                Id = dettaglioIdCounter++,
                RicevutaId = i,
                ProdottoId = secondoProdottoId,
                Quantita = 2,
                PrezzoTotale = (decimal)secondoProdottoId * 2,
                IsDeleted = false,
                CreatedAt = new DateTime(2024, 1, 1),
                ModifiedAt = new DateTime(2024, 1, 1)
            });
        }
        modelBuilder.Entity<ReceiptDetail>().HasData(dettagli);
        // ====================================================================================================
    }
}