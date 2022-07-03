using Microsoft.EntityFrameworkCore;

namespace To_Do_List.Models
{
    public class ItemContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public ItemContext(DbContextOptions<ItemContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
