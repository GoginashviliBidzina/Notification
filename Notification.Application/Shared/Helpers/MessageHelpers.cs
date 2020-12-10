using Newtonsoft.Json;
using Azure.Messaging.ServiceBus;
using Notification.Application.Infrastructure;

namespace Notification.Application.Shared.Helpers
{
    public static class MessageHelpers
    {
        public static TEvent DeserializeMessage<TEvent>(this ProcessMessageEventArgs args) where TEvent : Event
            => JsonConvert.DeserializeObject<TEvent>(args.Message.Body.ToString());
    }
}
