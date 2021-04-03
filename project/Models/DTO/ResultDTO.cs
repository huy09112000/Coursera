using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Models.DTO
{
    public class ResultDTO
    {
        public int QuesId { get; set; }
        public int AnsChoosed { get; set; }
        public int CorrectAns { get; set; }
        public int totalPoint { get; set; }
        public int QuizzId { get; set; }
    }
}