namespace project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updtepointtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Points", "CorrectAnswer", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Points", "CorrectAnswer");
        }
    }
}
