namespace project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renew : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        IsCorrect = c.Boolean(nullable: false),
                        CurrentQuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.CurrentQuestionId, cascadeDelete: true)
                .Index(t => t.CurrentQuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Point = c.Double(nullable: false),
                        Content = c.String(),
                        CurrentQuizzId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quizzs", t => t.CurrentQuizzId, cascadeDelete: true)
                .Index(t => t.CurrentQuizzId);
            
            CreateTable(
                "dbo.Quizzs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.String(),
                        Describtion = c.String(),
                        NumberQuestion = c.Int(),
                        Level = c.Int(),
                        time = c.Double(nullable: false),
                        CurrentLessionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lessions", t => t.CurrentLessionId, cascadeDelete: true)
                .Index(t => t.CurrentLessionId);
            
            CreateTable(
                "dbo.Lessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        Describtion = c.String(),
                        Url = c.String(),
                        View = c.Int(),
                        Content = c.String(),
                        CurentSubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.CurentSubjectId, cascadeDelete: true)
                .Index(t => t.CurentSubjectId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        Image = c.String(),
                        Describtion = c.String(),
                        Rate = c.Double(),
                        Total_rate = c.Int(),
                        CurrentCourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CurrentCourseId, cascadeDelete: true)
                .Index(t => t.CurrentCourseId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        Describtion = c.String(),
                        Code = c.String(maxLength: 10),
                        Image = c.String(),
                        rate = c.Double(),
                        total_rate = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserInfors",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(maxLength: 50),
                        DOB = c.DateTime(),
                        Phone = c.Int(),
                        Gender = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Points",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Double(nullable: false),
                        CorrectAnswer = c.Int(nullable: false),
                        Quizz_Id = c.Int(nullable: false),
                        UserInfor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quizzs", t => t.Quizz_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserInfors", t => t.UserInfor_Id, cascadeDelete: true)
                .Index(t => t.Quizz_Id)
                .Index(t => t.UserInfor_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        Role = c.Int(nullable: false),
                        Avatar = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Feature_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.COURSE_INFOR",
                c => new
                    {
                        UserInforRef_ID = c.Int(nullable: false),
                        CourseRef_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserInforRef_ID, t.CourseRef_ID })
                .ForeignKey("dbo.UserInfors", t => t.UserInforRef_ID, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseRef_ID, cascadeDelete: true)
                .Index(t => t.UserInforRef_ID)
                .Index(t => t.CourseRef_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "CurrentQuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "CurrentQuizzId", "dbo.Quizzs");
            DropForeignKey("dbo.Quizzs", "CurrentLessionId", "dbo.Lessions");
            DropForeignKey("dbo.Lessions", "CurentSubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "CurrentCourseId", "dbo.Courses");
            DropForeignKey("dbo.UserInfors", "Id", "dbo.Users");
            DropForeignKey("dbo.Features", "CurrentUserId", "dbo.Users");
            DropForeignKey("dbo.Features", "CurrentGroupFeatureId", "dbo.GroupFeatures");
            DropForeignKey("dbo.Points", "UserInfor_Id", "dbo.UserInfors");
            DropForeignKey("dbo.Points", "Quizz_Id", "dbo.Quizzs");
            DropForeignKey("dbo.COURSE_INFOR", "CourseRef_ID", "dbo.Courses");
            DropForeignKey("dbo.COURSE_INFOR", "UserInforRef_ID", "dbo.UserInfors");
            DropIndex("dbo.COURSE_INFOR", new[] { "CourseRef_ID" });
            DropIndex("dbo.COURSE_INFOR", new[] { "UserInforRef_ID" });
            DropIndex("dbo.Features", new[] { "CurrentUserId" });
            DropIndex("dbo.Features", new[] { "CurrentGroupFeatureId" });
            DropIndex("dbo.Points", new[] { "UserInfor_Id" });
            DropIndex("dbo.Points", new[] { "Quizz_Id" });
            DropIndex("dbo.UserInfors", new[] { "Id" });
            DropIndex("dbo.Subjects", new[] { "CurrentCourseId" });
            DropIndex("dbo.Lessions", new[] { "CurentSubjectId" });
            DropIndex("dbo.Quizzs", new[] { "CurrentLessionId" });
            DropIndex("dbo.Questions", new[] { "CurrentQuizzId" });
            DropIndex("dbo.Answers", new[] { "CurrentQuestionId" });
            DropTable("dbo.COURSE_INFOR");
            DropTable("dbo.GroupFeatures");
            DropTable("dbo.Features");
            DropTable("dbo.Users");
            DropTable("dbo.Points");
            DropTable("dbo.UserInfors");
            DropTable("dbo.Courses");
            DropTable("dbo.Subjects");
            DropTable("dbo.Lessions");
            DropTable("dbo.Quizzs");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
