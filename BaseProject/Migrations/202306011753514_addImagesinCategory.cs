namespace BaseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImagesinCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Images", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "Images");
        }
    }
}
