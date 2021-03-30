using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Models.DTO
{
    public class QuizzDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Describtion { get; set; }
        public int? NumberQuestion { get; set; }
        public int? Level { get; set; }
        public double time { get; set; }

        public int CurrentLessionId { get; set; }
    }
}