using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project.Models
{
    [Table("Courses")]
    public class Course
    {
        public Course()
        {
            this.UserInfors = new HashSet<UserInfor>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        public string Describtion { get; set; }
        [MaxLength(10)]
        public string Code { get; set; }

        public string Image { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Rate must be  positive")]
        public double? rate { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Rate must be  positive")]
        public int? total_rate { get; set; }

        public virtual ICollection<UserInfor> UserInfors { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}