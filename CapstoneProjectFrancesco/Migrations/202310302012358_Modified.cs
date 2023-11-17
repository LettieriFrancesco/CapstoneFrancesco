namespace CapstoneProjectFrancesco.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modified : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Scaffale_Prodotti", "IdScaffale", "dbo.Scaffale_Magazzino");
            DropPrimaryKey("dbo.Scaffale_Magazzino");
            AlterColumn("dbo.Scaffale_Magazzino", "IdScaffale", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Scaffale_Magazzino", "IdScaffale");
            AddForeignKey("dbo.Scaffale_Prodotti", "IdScaffale", "dbo.Scaffale_Magazzino", "IdScaffale");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Scaffale_Prodotti", "IdScaffale", "dbo.Scaffale_Magazzino");
            DropPrimaryKey("dbo.Scaffale_Magazzino");
            AlterColumn("dbo.Scaffale_Magazzino", "IdScaffale", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Scaffale_Magazzino", "IdScaffale");
            AddForeignKey("dbo.Scaffale_Prodotti", "IdScaffale", "dbo.Scaffale_Magazzino", "IdScaffale");
        }
    }
}
