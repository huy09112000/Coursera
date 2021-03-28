using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Models.DTO
{
    public class LessonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Describtion { get; set; }
        public string Url { get; set; }
        public int? View { get; set; }
        public string Content { get; set; }
        public int CurentSubjectId { get; set; }
    }
}