using System;

namespace Notification.Domain.NotificationManagement
{
    public class Notification
    {
        public int Id { get; private set; }

        public string Message { get; private set; }

        public DateTime OccuredOn { get; private set; }

        public Notification()
        {

        }

        public Notification(string message,
                            DateTime occuredOn)
        {
            Message = message;
            OccuredOn = occuredOn;
        }
    }
}
