using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Models.DTO
{
    public class AnswerDTO
    {
        public int Id { get; set; }

        public string Content { get; set; }
        public bool IsCorrect { get; set; }
    }
}