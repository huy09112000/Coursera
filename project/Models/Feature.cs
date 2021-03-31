using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project.Models
{
    [Table("Features")]
    public class Feature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeatureId { get; set; }
        [MaxLength(100)]
        public string Url { get; set; }

        public int? CurrentGroupFeatureId { get; set; }
        public GroupFeature GroupFeature { get; set; }

        public int? CurrentUserId { get; set; }
        public User User { get; set; }
    }
}