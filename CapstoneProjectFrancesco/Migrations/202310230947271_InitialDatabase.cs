namespace CapstoneProjectFrancesco.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aziende",
                c => new
                    {
                        IdAzienda = c.Int(nullable: false, identity: true),
                        NomeAzienda = c.String(nullable: false),
                        IndirizzoAzienda = c.String(),
                        RecapitoAzienda = c.String(),
                        DescrizioneAzienda = c.String(),
                        FotoAzienda = c.String(),
                        LogoAzienda = c.String(),
                    })
                .PrimaryKey(t => t.IdAzienda);
            
            CreateTable(
                "dbo.Prodotti",
                c => new
                    {
                        IdProdotto = c.Int(nullable: false, identity: true),
                        NomeProdotto = c.String(nullable: false),
                        DescrizioneProdotto = c.String(nullable: false),
                        DescrizioneLavorazione = c.String(),
                        Disponibilita = c.Boolean(),
                        Ingredienti = c.String(),
                        Conservazione = c.String(),
                        Origine = c.String(),
                        Confezionamento = c.String(),
                        Peso = c.String(),
                        FotoProdotto1 = c.String(),
                        FotoProdotto2 = c.String(),
                        FotoProdotto3 = c.String(),
                        Prezzo = c.Decimal(nullable: false, storeType: "money"),
                        IdAzienda = c.Int(nullable: false),
                        IdCategoria = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdProdotto)
                .ForeignKey("dbo.Categorie", t => t.IdCategoria)
                .ForeignKey("dbo.Aziende", t => t.IdAzienda)
                .Index(t => t.IdAzienda)
                .Index(t => t.IdCategoria);
            
            CreateTable(
                "dbo.Categorie",
                c => new
                    {
                        IdCategoria = c.Int(nullable: false, identity: true),
                        CategoriaProdotto = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdCategoria);
            
            CreateTable(
                "dbo.Dettaglio_Ordine",
                c => new
                    {
                        IdDettaglioOrdine = c.Int(nullable: false, identity: true),
                        Quantita = c.Int(nullable: false),
                        IdProdotto = c.Int(nullable: false),
                        IdOrdine = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdDettaglioOrdine)
                .ForeignKey("dbo.Ordine", t => t.IdOrdine)
                .ForeignKey("dbo.Prodotti", t => t.IdProdotto)
                .Index(t => t.IdProdotto)
                .Index(t => t.IdOrdine);
            
            CreateTable(
                "dbo.Ordine",
                c => new
                    {
                        IdOrdine = c.Int(nullable: false, identity: true),
                        Evaso = c.Boolean(nullable: false),
                        Concluso = c.Boolean(nullable: false),
                        Data = c.DateTime(nullable: false, storeType: "date"),
                        TempoConsegna = c.String(),
                        Commento = c.String(),
                        Indirizzo = c.String(),
                        Importo = c.Decimal(nullable: false, storeType: "money"),
                        Spedizione = c.Boolean(),
                        ImportoSpedizione = c.Int(),
                        IdUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdOrdine)
                .ForeignKey("dbo.User", t => t.IdUser)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        IdUser = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Cognome = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 80),
                        Password = c.String(nullable: false, maxLength: 50),
                        Ruolo = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IdUser);
            
            CreateTable(
                "dbo.Scaffale_Prodotti",
                c => new
                    {
                        IdScaffaleProdotti = c.Int(nullable: false, identity: true),
                        IdScaffale = c.Int(nullable: false),
                        IdProdotto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdScaffaleProdotti)
                .ForeignKey("dbo.Scaffale_Magazzino", t => t.IdScaffale)
                .ForeignKey("dbo.Prodotti", t => t.IdProdotto)
                .Index(t => t.IdScaffale)
                .Index(t => t.IdProdotto);
            
            CreateTable(
                "dbo.Scaffale_Magazzino",
                c => new
                    {
                        IdScaffale = c.Int(nullable: false),
                        DescrizioneScaffale = c.String(nullable: false),
                        IdCorsia = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdScaffale)
                .ForeignKey("dbo.Corsie_Magazzino", t => t.IdCorsia)
                .Index(t => t.IdCorsia);
            
            CreateTable(
                "dbo.Corsie_Magazzino",
                c => new
                    {
                        IdCorsia = c.Int(nullable: false, identity: true),
                        DescrizioneCorsia = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdCorsia);
            
            CreateTable(
                "dbo.Richeste_Clienti",
                c => new
                    {
                        IdRichiesta = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Cognome = c.String(),
                        Email = c.String(),
                        NumeroTelefono = c.String(),
                        MessaggioRichiesta = c.String(),
                    })
                .PrimaryKey(t => t.IdRichiesta);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prodotti", "IdAzienda", "dbo.Aziende");
            DropForeignKey("dbo.Scaffale_Prodotti", "IdProdotto", "dbo.Prodotti");
            DropForeignKey("dbo.Scaffale_Prodotti", "IdScaffale", "dbo.Scaffale_Magazzino");
            DropForeignKey("dbo.Scaffale_Magazzino", "IdCorsia", "dbo.Corsie_Magazzino");
            DropForeignKey("dbo.Dettaglio_Ordine", "IdProdotto", "dbo.Prodotti");
            DropForeignKey("dbo.Ordine", "IdUser", "dbo.User");
            DropForeignKey("dbo.Dettaglio_Ordine", "IdOrdine", "dbo.Ordine");
            DropForeignKey("dbo.Prodotti", "IdCategoria", "dbo.Categorie");
            DropIndex("dbo.Scaffale_Magazzino", new[] { "IdCorsia" });
            DropIndex("dbo.Scaffale_Prodotti", new[] { "IdProdotto" });
            DropIndex("dbo.Scaffale_Prodotti", new[] { "IdScaffale" });
            DropIndex("dbo.Ordine", new[] { "IdUser" });
            DropIndex("dbo.Dettaglio_Ordine", new[] { "IdOrdine" });
            DropIndex("dbo.Dettaglio_Ordine", new[] { "IdProdotto" });
            DropIndex("dbo.Prodotti", new[] { "IdCategoria" });
            DropIndex("dbo.Prodotti", new[] { "IdAzienda" });
            DropTable("dbo.Richeste_Clienti");
            DropTable("dbo.Corsie_Magazzino");
            DropTable("dbo.Scaffale_Magazzino");
            DropTable("dbo.Scaffale_Prodotti");
            DropTable("dbo.User");
            DropTable("dbo.Ordine");
            DropTable("dbo.Dettaglio_Ordine");
            DropTable("dbo.Categorie");
            DropTable("dbo.Prodotti");
            DropTable("dbo.Aziende");
        }
    }
}
