using datagenerator.core.models;
using Microsoft.EntityFrameworkCore;

namespace datagenerator.core.repository
{
    public sealed class DatabaseContext : DbContext
    {
        DbSet<Item> Items { get; set; } = null!;
        DbSet<User> Users { get; set; } = null!;
        DbSet<Template> Templates { get; set; } = null!;
        DbSet<UserData> UserData { get; set; } = null!;

        public DatabaseContext(DbContextOptions<DatabaseContext> options) 
            : base(options) { }
    }
}
