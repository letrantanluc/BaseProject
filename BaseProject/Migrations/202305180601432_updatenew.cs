﻿namespace BaseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatenew : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Gender", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Gender", c => c.Boolean(nullable: false));
        }
    }
}
