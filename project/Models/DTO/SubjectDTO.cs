using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Models.DTO
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Image { get; set; }
        public string Describtion { get; set; }

        public double? Rate { get; set; }
        public int? Total_rate { get; set; }
        public int CurrentCourseId { get; set; }
    }
}