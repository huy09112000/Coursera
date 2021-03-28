namespace project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class empty : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subjects", "Rate", c => c.Double());
            AlterColumn("dbo.Courses", "rate", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "rate", c => c.Int());
            AlterColumn("dbo.Subjects", "Rate", c => c.Int());
        }
    }
}
