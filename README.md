# Progetto_Finale_Gruppo3

StockManager Pro Sistema Gestionale per Operatori di Magazzino
Stack tecnologico
C#  •  .NET 8  •  Entity Framework Core  •  LINQ  •  MySQL  •  Docker Desktop

1. Descrizione del progetto
Lo scopo del progetto è realizzare un gestionale pensato per il magazziniere/amministratore di un e-commerce. Il software simula il backoffice interno di una piattaforma di vendita online: l’operatore usa l’applicazione per registrare clienti, gestire il catalogo prodotti, processare ordini e monitorare le vendite, il tutto in modo guidato attraverso un’interfaccia per operazioni CRUD. 
Da valutare: sistema di autenticazione per ruoli multipli: il magazziniere e l’amministratore.

2. Funzionalità dell’applicazione
L’operatore, una volta avviata l’applicazione, deve poter navigare tra quattro aree principali: clienti, prodotti, ordini e log.
    2.1 Gestione clienti
L’operatore può inserire un nuovo cliente fornendo nome, cognome, e-mail e telefono. L’email deve essere univoca: se viene inserita un’e-mail già presente nel database, il sistema deve mostrare un errore. I clienti possono essere cercati per cognome o e-mail, modificati e, se non hanno ordini associati, eliminati.
    2.2 Gestione prodotti
Il catalogo prodotti è gestito dall’operatore: può aggiungere nuovi articoli con nome, descrizione, categoria, prezzo base e quantità in magazzino. I prodotti possono essere modificati (in particolare il prezzo e la disponibilità) e visualizzati con filtro per categoria. Durante la creazione di un ordine, il prodotto può essere avvolto da uno o più Decorator che ne modificano il costo finale.
    2.3 Report e storico
L’operatore può visualizzare lo storico degli ordini filtrato per cliente, con il totale speso da ciascuno. Può inoltre vedere la lista degli ordini del giorno con la somma totale. 

3. Design Pattern
    3.1 Singleton: Per la raccolta dei log dell’applicazione. 
    3.2 Factory: Per la generazione di entità: persona, cliente, dipendente.
    3.3 Decorator: Per il prodotto, apportando modifiche di prezzo e di descrizione. 
    3.4 Observer: Per notificare un ordine effettuato e un checkout di pagamento. 
    3.5 Strategy: Per gestire la modalità di pagamento (Paypal, Bitcoin, Contanti e Carta di credito) 
    In sintesi: Decorator calcola il prezzo. Strategy esegue il pagamento. Observer notifica i tre operatori. Factory ha creato i logger all’avvio. Singleton garantisce che ci sia un solo punto di log in tutto il sistema.
