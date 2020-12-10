using System;

namespace Notification.Application.Notifications
{
    public class DefaultEventModel : Infrastructure.Event
    {
        public string Message { get; set; }

        public DateTime OccuredOn { get; set; }
    }
}
