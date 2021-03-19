using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project.Models
{
    [Table("Questions")]
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Url { get; set; }
        public int? Point { get; set; }
        public string Content { get; set; }
        public int CurrentQuizzId { get; set; }
        public Quizz Quizz { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}