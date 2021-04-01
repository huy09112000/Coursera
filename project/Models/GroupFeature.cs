using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project.Models
{   
    [Table("GroupFeatures")]
    public class GroupFeature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,Range(0,3)]
        public int Role { get; set; }
        public int Feature_id { get; set; }

        public ICollection<Feature>features { get; set; }
    }
}