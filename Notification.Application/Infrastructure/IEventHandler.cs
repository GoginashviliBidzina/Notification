using System.Threading.Tasks;

namespace Notification.Application.Infrastructure
{
    public interface IEventHandler<in TEvent> where TEvent : Event
    {
        Task Handle(TEvent @event);
    }
}
