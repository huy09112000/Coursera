using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Models.DTO
{
    public class PointAfterSubmitDTO
    {
        public int Id { get; set; }

        public double Score { get; set; }
        public int CorrectAnswer { get; set; }
        public virtual UserInfor UserInfor { get; set; }

        public virtual Quizz Quizz { get; set; }
    }
}