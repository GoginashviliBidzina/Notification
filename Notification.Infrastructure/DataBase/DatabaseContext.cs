using Microsoft.EntityFrameworkCore;
using Notification.Infrastructure.DataBase.Configurations;

namespace Notification.Infrastructure.DataBase
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new NotificationConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
