using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
        public string Avatar { get; set; }
    }
}