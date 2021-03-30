namespace project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changquiz : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizzs", "time", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quizzs", "time");
        }
    }
}
