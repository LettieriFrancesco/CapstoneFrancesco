namespace CapstoneProjectFrancesco.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatoOrdine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ordine", "StatoOrdine", c => c.String());
            DropColumn("dbo.Ordine", "Evaso");
            DropColumn("dbo.Ordine", "Concluso");
            DropColumn("dbo.Ordine", "CommentoOrdine");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ordine", "CommentoOrdine", c => c.String());
            AddColumn("dbo.Ordine", "Concluso", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ordine", "Evaso", c => c.Boolean(nullable: false));
            DropColumn("dbo.Ordine", "StatoOrdine");
        }
    }
}
