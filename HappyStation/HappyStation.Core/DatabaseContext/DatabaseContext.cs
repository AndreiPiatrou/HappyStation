using System.Configuration;
using System.Data.Entity;

using HappyStation.Core.Entities;

namespace HappyStation.Core.DatabaseContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
        {
        }

        public DatabaseContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<News> News { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<PhotoAlbum> PhotoAlbums { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
