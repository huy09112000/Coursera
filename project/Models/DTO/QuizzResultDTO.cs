using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Models.DTO
{
    public class QuizzResultDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? Level { get; set; }
        public double time { get; set; }
        public double Score { get; set; }
        public int CorrectAnswer { get; set; }
    }
}