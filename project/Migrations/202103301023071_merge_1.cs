namespace project.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class merge_1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Questions", "Point", c => c.Double(nullable: false));
            AlterColumn("dbo.Points", "Score", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Points", "Score", c => c.Int(nullable: false));
            AlterColumn("dbo.Questions", "Point", c => c.Int());
        }
    }
}
