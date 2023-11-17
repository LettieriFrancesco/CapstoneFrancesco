namespace CapstoneProjectFrancesco.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabellaOrdine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ordine", "CommentoOrdine", c => c.String());
            DropColumn("dbo.Ordine", "Commento");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ordine", "Commento", c => c.String());
            DropColumn("dbo.Ordine", "CommentoOrdine");
        }
    }
}
