using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace userDashboard.Models
{
    public class Post : BaseEntity
    {
        public int id { get; set; }
        public int userid { get; set; }
        public User user { get; set; }
        [Required]
        [MinLength(10)]
        public string content { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }
        public List<Comment> comments { get; set; }
        public Post()
        {
            comments = new List<Comment>();
        }
    }
}