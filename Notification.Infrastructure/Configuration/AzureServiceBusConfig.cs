﻿namespace Notification.Infrastructure.Configuration
{
    public class AzureServiceBusConfig
    {
        public string ConnectionString { get; set; }

        public string QueueName { get; set; }
    }
}
