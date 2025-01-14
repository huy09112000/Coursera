﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project.Models
{
    [Table("Quizzs")]
    public class Quizz
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Describtion { get; set; }
        [Range(0,int.MaxValue,ErrorMessage ="Field must positive")]
        public int? NumberQuestion { get; set; }
        [Range(0, 5, ErrorMessage = "Level must between 0 and 5")]
        public int? Level { get; set; }
        [Range(0,double.MaxValue,ErrorMessage ="Time must be positive")]
        public double time { get; set; }

        public int CurrentLessionId { get; set; }
        public Lession Lession { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Point> Points { get; set; }
    }
}