using project.DAL.Seed;
using project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace project.DAL
{
    public class EducationDBContext : DbContext
    {
        public EducationDBContext() : base("name=Education")
        {
            //Database.SetInitializer(new EducationInitializer());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserInfor> UserInfors { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Lession> Lessions { get; set; }

        public DbSet<Quizz> Quizzs { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Point> points { get; set; }

        public DbSet<Feature> features { get; set; }
        public DbSet<GroupFeature> groupFeatures { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<User>().HasOptional(u => u.UserInfor).WithRequired(ui => ui.User);
            modelBuilder.Entity<UserInfor>()
                .HasMany<Course>(ui => ui.Courses)
                .WithMany(c => c.UserInfors)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserInforRef_ID");
                    cs.MapRightKey("CourseRef_ID");
                    cs.ToTable("COURSE_INFOR");
                });

            modelBuilder.Entity<Subject>()
                .HasRequired<Course>(s => s.Course)
                .WithMany(c => c.Subjects)
                .HasForeignKey<int>(s => s.CurrentCourseId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Lession>()
                .HasRequired<Subject>(l => l.Subject)
                .WithMany(s => s.Lessions)
                .HasForeignKey<int>(l => l.CurentSubjectId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Quizz>()
                .HasRequired<Lession>(q => q.Lession)
                .WithMany(l => l.Quizzs)
                .HasForeignKey<int>(q => q.CurrentLessionId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Question>()
                .HasRequired<Quizz>(q => q.Quizz)
                .WithMany(qi => qi.Questions)
                .HasForeignKey<int>(q => q.CurrentQuizzId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Answer>()
                .HasRequired<Question>(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey<int>(a => a.CurrentQuestionId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Point>()
                 .HasRequired<UserInfor>(p => p.UserInfor)
                 .WithMany(uf => uf.Points);

            modelBuilder.Entity<Point>()
                .HasRequired<Quizz>(p => p.Quizz)
                .WithMany(qi => qi.Points);
            modelBuilder.Entity<Feature>()
                .HasOptional<GroupFeature>(f => f.GroupFeature)
                .WithMany(gf => gf.features)
                .HasForeignKey<int?>(f => f.CurrentGroupFeatureId);
            modelBuilder.Entity<Feature>()
                .HasOptional<User>(f => f.User)
                .WithMany(u => u.features)
                .HasForeignKey<int?>(f=>f.CurrentUserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}