namespace CapstoneProjectFrancesco.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class descrizionelavorazioneEliminata : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Prodotti", "DescrizioneLavorazione");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prodotti", "DescrizioneLavorazione", c => c.String());
        }
    }
}
