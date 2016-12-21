using System;
using System.ComponentModel.DataAnnotations;
namespace userDashboard.Models
{
    public class Comment : BaseEntity
    {
        public int id { get; set; }
        public int postid { get; set; }
        public Post post { get; set; }
        public int userid { get; set; }
        public User user { get; set; }
        [Required]
        [MinLength(2)]
        public string commentcontent { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }
    }
}
