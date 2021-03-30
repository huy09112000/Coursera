namespace project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImageQuizz : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizzs", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quizzs", "Image");
        }
    }
}
