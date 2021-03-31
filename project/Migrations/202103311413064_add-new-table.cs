namespace project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        FeatureId = c.Int(nullable: false, identity: true),
                        Url = c.String(maxLength: 100),
                        CurrentGroupFeatureId = c.Int(),
                        CurrentUserId = c.Int(),
                    })
                .PrimaryKey(t => t.FeatureId)
                .ForeignKey("dbo.GroupFeatures", t => t.CurrentGroupFeatureId)
                .ForeignKey("dbo.Users", t => t.CurrentUserId)
                .Index(t => t.CurrentGroupFeatureId)
                .Index(t => t.CurrentUserId);
            
            CreateTable(
                "dbo.GroupFeatures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Features", "CurrentUserId", "dbo.Users");
            DropForeignKey("dbo.Features", "CurrentGroupFeatureId", "dbo.GroupFeatures");
            DropIndex("dbo.Features", new[] { "CurrentUserId" });
            DropIndex("dbo.Features", new[] { "CurrentGroupFeatureId" });
            DropTable("dbo.GroupFeatures");
            DropTable("dbo.Features");
        }
    }
}
