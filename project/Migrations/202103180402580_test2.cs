namespace project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Infor_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Infor_id", c => c.Int());
        }
    }
}
