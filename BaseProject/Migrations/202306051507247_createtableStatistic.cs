namespace BaseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createtableStatistic : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Statistics", newName: "Analytics");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Analytics", newName: "Statistics");
        }
    }
}
