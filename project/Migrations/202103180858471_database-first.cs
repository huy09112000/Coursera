namespace project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databasefirst : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UserInfors", new[] { "ID" });
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
                        Point = c.Int(),
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
                        Describtion = c.String(),
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
                        Rate = c.Int(),
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
                        rate = c.Int(),
                        total_rate = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Points",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                        Quizz_Id = c.Int(nullable: false),
                        UserInfor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quizzs", t => t.Quizz_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserInfors", t => t.UserInfor_Id, cascadeDelete: true)
                .Index(t => t.Quizz_Id)
                .Index(t => t.UserInfor_Id);
            
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
            
            AlterColumn("dbo.UserInfors", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Users", "Avatar", c => c.String());
            CreateIndex("dbo.UserInfors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "CurrentQuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "CurrentQuizzId", "dbo.Quizzs");
            DropForeignKey("dbo.Quizzs", "CurrentLessionId", "dbo.Lessions");
            DropForeignKey("dbo.Lessions", "CurentSubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "CurrentCourseId", "dbo.Courses");
            DropForeignKey("dbo.Points", "UserInfor_Id", "dbo.UserInfors");
            DropForeignKey("dbo.Points", "Quizz_Id", "dbo.Quizzs");
            DropForeignKey("dbo.COURSE_INFOR", "CourseRef_ID", "dbo.Courses");
            DropForeignKey("dbo.COURSE_INFOR", "UserInforRef_ID", "dbo.UserInfors");
            DropIndex("dbo.COURSE_INFOR", new[] { "CourseRef_ID" });
            DropIndex("dbo.COURSE_INFOR", new[] { "UserInforRef_ID" });
            DropIndex("dbo.Points", new[] { "UserInfor_Id" });
            DropIndex("dbo.Points", new[] { "Quizz_Id" });
            DropIndex("dbo.UserInfors", new[] { "Id" });
            DropIndex("dbo.Subjects", new[] { "CurrentCourseId" });
            DropIndex("dbo.Lessions", new[] { "CurentSubjectId" });
            DropIndex("dbo.Quizzs", new[] { "CurrentLessionId" });
            DropIndex("dbo.Questions", new[] { "CurrentQuizzId" });
            DropIndex("dbo.Answers", new[] { "CurrentQuestionId" });
            AlterColumn("dbo.Users", "Avatar", c => c.Int());
            AlterColumn("dbo.UserInfors", "Name", c => c.String());
            DropTable("dbo.COURSE_INFOR");
            DropTable("dbo.Points");
            DropTable("dbo.Courses");
            DropTable("dbo.Subjects");
            DropTable("dbo.Lessions");
            DropTable("dbo.Quizzs");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
            CreateIndex("dbo.UserInfors", "ID");
        }
    }
}
