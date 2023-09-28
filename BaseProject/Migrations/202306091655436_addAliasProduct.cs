namespace BaseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAliasProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Alias", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Alias");
        }
    }
}
