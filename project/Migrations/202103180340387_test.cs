namespace project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInfors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DOB = c.DateTime(),
                        Phone = c.Int(),
                        Gender = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        Role = c.Int(nullable: false),
                        Avatar = c.Int(),
                        Infor_id = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInfors", "ID", "dbo.Users");
            DropIndex("dbo.UserInfors", new[] { "ID" });
            DropTable("dbo.Users");
            DropTable("dbo.UserInfors");
        }
    }
}
