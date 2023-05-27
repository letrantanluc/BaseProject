namespace BaseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAliasinNews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Alias", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "Alias");
        }
    }
}
