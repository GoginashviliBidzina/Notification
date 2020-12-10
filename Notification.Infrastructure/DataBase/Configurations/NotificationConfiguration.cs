using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Notification.Infrastructure.DataBase.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Domain.NotificationManagement.Notification>
    {
        public void Configure(EntityTypeBuilder<Domain.NotificationManagement.Notification> builder)
        {
            builder.ToTable(nameof(Domain.NotificationManagement.Notification));
        }
    }
}
