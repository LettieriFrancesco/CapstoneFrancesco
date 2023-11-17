namespace CapstoneProjectFrancesco.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quantitaTableProdotti : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prodotti", "Quantita", c => c.Int(nullable: false));
            DropColumn("dbo.Prodotti", "Disponibilita");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prodotti", "Disponibilita", c => c.String());
            DropColumn("dbo.Prodotti", "Quantita");
        }
    }
}
