using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project.Models
{
    [Table("Lessions")]
    public class Lession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        public string Describtion { get; set; }
        public string Url { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "View must be  positive")]

        public int? View { get; set; }
        public string Content { get; set; }
        public int CurentSubjectId { get; set; }
        public Subject Subject { get; set; }

        public bool? Learned { get; set; }
        public virtual ICollection<Quizz> Quizzs { get; set; }
    }
}