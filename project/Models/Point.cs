﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project.Models
{
    [Table("Points")]
    public class Point
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Range(0,double.MaxValue,ErrorMessage ="point must positive"),Required]

        public double Score { get; set; }
        public int CorrectAnswer { get; set; }
        public virtual UserInfor UserInfor { get; set; }
  
        public virtual Quizz Quizz { get; set; }
    }
}