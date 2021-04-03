namespace project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBcontext : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lessions", "Learned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lessions", "Learned");
        }
    }
}
