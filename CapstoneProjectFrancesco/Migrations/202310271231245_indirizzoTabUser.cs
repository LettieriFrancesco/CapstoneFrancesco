﻿namespace CapstoneProjectFrancesco.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class indirizzoTabUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Indirizzo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Indirizzo");
        }
    }
}
