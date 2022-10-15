using Microsoft.EntityFrameworkCore;

namespace To_Do_List.Models
{
    public class To_DoContext : DbContext
    {
        public To_DoContext(DbContextOptions<To_DoContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Item> Items { get; set; }
    }    
}
