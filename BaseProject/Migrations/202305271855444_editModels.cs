namespace BaseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "Title", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.News", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.News", "Detail", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Payment", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Payment", c => c.String());
            AlterColumn("dbo.News", "Detail", c => c.String());
            AlterColumn("dbo.News", "Description", c => c.String());
            AlterColumn("dbo.News", "Title", c => c.String());
        }
    }
}
