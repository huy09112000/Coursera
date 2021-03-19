using project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace project.DAL.Seed
{
    public class EducationInitializer : DropCreateDatabaseAlways<EducationDBContext>
    {
        protected override void Seed(EducationDBContext context)
        {
            IList<User> users = new List<User>();

            users.Add(new User() { Id = 1, Email = "Admin", Password = "1", Role = 1 });
            users.Add(new User() { Id = 2, Email = "Student", Password = "1", Role = 2 });
            users.Add(new User() { Id = 3, Email = "Teacher", Password = "1", Role = 3 });

            IList<UserInfor> userInfors = new List<UserInfor>();

            userInfors.Add(new UserInfor() { Name = "Admin", User = { Id = 1, Email = "Admin", Password = "1", Role = 1 } });
            userInfors.Add(new UserInfor() { Name = "Student", User = { Id = 2, Email = "Student", Password = "1", Role = 2 } });
            userInfors.Add(new UserInfor() { Name = "Teacher", User = { Id = 3, Email = "Teacher", Password = "1", Role = 3 } });

            context.Users.AddRange(users);
            context.UserInfors.AddRange(userInfors);
            base.Seed(context);
        }
    }
}