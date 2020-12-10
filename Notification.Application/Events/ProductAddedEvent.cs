using System;

namespace Notification.Application.Notifications
{
    public class ProductAddedEvent : Infrastructure.Event
    {
        public int AggregateRootId { get; set; }

        public string Message { get; set; }

        public DateTime OccuredOn { get; set; }
    }
}
