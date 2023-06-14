namespace BaseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrequiredProduct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "image", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Status", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Location", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Location", c => c.String());
            AlterColumn("dbo.Products", "Status", c => c.String());
            AlterColumn("dbo.Products", "image", c => c.String());
        }
    }
}
