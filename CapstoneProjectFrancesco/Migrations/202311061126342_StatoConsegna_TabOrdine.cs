namespace CapstoneProjectFrancesco.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatoConsegna_TabOrdine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ordine", "StatoConsegna", c => c.String());
            DropColumn("dbo.Ordine", "TempoConsegna");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ordine", "TempoConsegna", c => c.String());
            DropColumn("dbo.Ordine", "StatoConsegna");
        }
    }
}
