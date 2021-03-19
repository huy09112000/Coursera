using System;
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

        public string Describtion { get; set; }

        public int CurrentLessionId { get; set; }
        public Lession Lession { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Point> Points { get; set; }
    }
}