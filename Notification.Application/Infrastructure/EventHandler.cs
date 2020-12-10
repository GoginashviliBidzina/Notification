using System;
using System.Threading.Tasks;
using Notification.Application.Notifications;
using Notification.Infrastructure.Repository;

namespace Notification.Application.Infrastructure
{
    public class EventHandler : IEventHandler<ProductAddedEvent>,
                                IEventHandler<DefaultEventModel>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRepository<Domain.NotificationManagement.Notification> _notificationRepository;

        public EventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _notificationRepository = GetService<IRepository<Domain.NotificationManagement.Notification>>();

        }
        public T GetService<T>() => (T)_serviceProvider.GetService(typeof(T));

        public async Task Handle(ProductAddedEvent @event)
            => await _notificationRepository.AddAsync(new Domain.NotificationManagement.Notification(@event.Message,
                                                                                                     @event.OccuredOn));

        public async Task Handle(DefaultEventModel @event)
            => await _notificationRepository.AddAsync(new Domain.NotificationManagement.Notification(@event.Message,
                                                                                                     @event.OccuredOn));
    }
}
