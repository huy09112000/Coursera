namespace project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeQuizz : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizzs", "Name", c => c.String());
            AddColumn("dbo.Quizzs", "NumberQuestion", c => c.Int());
            AddColumn("dbo.Quizzs", "Level", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quizzs", "Level");
            DropColumn("dbo.Quizzs", "NumberQuestion");
            DropColumn("dbo.Quizzs", "Name");
        }
    }
}
