﻿namespace BaseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrequiredEmailOrder : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Email", c => c.String());
        }
    }
}
