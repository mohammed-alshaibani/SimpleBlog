using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SimpleBlog.Models
{
    public class DbContextBlog: IdentityDbContext<AppUser>
    {
        public DbContextBlog()
        {

        }
        public DbContextBlog(DbContextOptions<DbContextBlog> options) : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; } = null!;
    }
}
