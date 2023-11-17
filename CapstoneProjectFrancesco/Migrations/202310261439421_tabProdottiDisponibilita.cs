namespace CapstoneProjectFrancesco.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tabProdottiDisponibilita : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Prodotti", "Disponibilita", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Prodotti", "Disponibilita", c => c.Boolean());
        }
    }
}
