﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Models.DTO
{
    public class QuestionSubmitDTO
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? Point { get; set; }
        public string Content { get; set; }
        public int CurrentQuizzId { get; set; }
        public int CurrentAnswerId { get; set; }
        
    }
}