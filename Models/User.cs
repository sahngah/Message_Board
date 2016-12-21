using System;
using System.Collections.Generic;
namespace userDashboard.Models
{
    public abstract class BaseEntity {}
    public class User: BaseEntity
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }
        public List<Post> posts { get; set; }
        public List<Comment> comments { get; set; }
        public User()
        {
            posts = new List<Post>();
            comments = new List<Comment>();
        }
    }
}