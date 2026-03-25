# Progetto Finale — Gruppo 3 | StockManager Pro
Sistema Gestionale per Operatori di Magazzino
Stack: C# · .NET 8 · Entity Framework Core · LINQ · MySQL · Docker Desktop

## Descrizione
Gestionale per magazzinieri e amministratori di un e-commerce. Il backend espone API REST per la gestione di persone, clienti, dipendenti, prodotti, scontrini e pagamenti. Il frontend è un'applicazione a terminale interattiva.

## Struttura del progetto
- store.core   : Dominio (entità, interfacce, Strategy di pagamento)
- store.api    : Backend REST (Controller, Service, Repository, Decorator, Facade)
- store.client : Frontend a terminale (Observer, Singleton, menu interattivo)

## Divisione del lavoro
COSIMO   : CRUD anagrafica, endpoint, Factory risposte, Strategy core, D.I., Auth
FABRIZIA : Decorator prodotti, scontrini (Receipt), PaymentController, ReceiptController, Facade, presentazione
MATTEO   : Singleton log, Observer notifiche, interfaccia terminale, report, storico, stockaggi,
ANDREA   : Revisione, Testing, Integrazione componenti

## Design Pattern
3.1 Singleton  : AppLogger — unico punto di log (store.client)
3.2 Factory    : ApiResponseFactory — risposte API standardizzate (store.api)
3.3 Decorator  : ProductDecorator — optional su prodotti: GiftWrap, Express, Assicurazione (store.api)
3.4 Observer   : OrderObserver/Publisher — notifica operatori a ogni ordine (store.client)
3.5 Strategy   : PaymentContext + 4 Strategy — Paypal, CreditCard, Bitcoin, Liquid (store.core)
3.6 Facade     : StoreFacade — interfaccia unica del ReceiptController verso il ReceiptService (store.api)

