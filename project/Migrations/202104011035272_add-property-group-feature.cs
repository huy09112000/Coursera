namespace project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpropertygroupfeature : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroupFeatures", "Feature_id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GroupFeatures", "Feature_id");
        }
    }
}
