

using System.ComponentModel.DataAnnotations;

namespace SimpleBlog.Models
{
    public class Post
    {
        [Key]
        public  int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

    }
}
