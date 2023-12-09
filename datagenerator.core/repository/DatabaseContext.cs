using datagenerator.core.models;
using Microsoft.EntityFrameworkCore;

namespace datagenerator.core.repository
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Template> Templates { get; set; } = null!;
        public DbSet<UserData> UserData { get; set; } = null!;

        public DatabaseContext(DbContextOptions<DatabaseContext> options) 
            : base(options) { }
    }
}
