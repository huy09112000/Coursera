namespace project.Migrations
{
    using project.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<project.DAL.EducationDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(project.DAL.EducationDBContext context)
        {
          
            var User = new List<User>
            {
                new User{ Email="admin@gmail.com",Password="1",Role=1},
                new User{ Email="student@gmail.com",Password="1",Role=2},
                new User{ Email="teacher@gmail.com",Password="1",Role=3},

            };
            User.ForEach(s => context.Users.AddOrUpdate(p => p.Email, s));
            context.SaveChanges();
        }
    }
}
