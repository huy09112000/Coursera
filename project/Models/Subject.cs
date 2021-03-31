using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project.Models
{
    [Table("Subjects")]
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Image { get; set; }
        public string Describtion { get; set; }
        [Range(0,double.MaxValue,ErrorMessage ="Rate must be  positive")]
        public double? Rate { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Rate must be  positive")]

        public int? Total_rate { get; set; }

        public int CurrentCourseId { get; set; }
        public Course Course { get; set; }

        public virtual ICollection<Lession> Lessions { get; set; }
    }
}