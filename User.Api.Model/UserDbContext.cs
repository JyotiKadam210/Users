using Microsoft.EntityFrameworkCore;

namespace Users.Api.Model
{
    public partial class UserDbContext : DbContext
    {
        public UserDbContext()
        {
            
        }
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            // optionsBuilder.UseSqlite("Data Source = C:\\Users\\Lenovo\\source\\repos\\Users\\User.Api.Model\\Database\\UserDb.db");


        }
    }
}
