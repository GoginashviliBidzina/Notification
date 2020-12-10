using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Notification.Application.Infrastructure
{
    public class EventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Dispatch<TEvent>(TEvent @event) where TEvent : Event
        {
            try
            {
                var handler = Assembly.GetExecutingAssembly()
                                      .GetTypes()
                                      .FirstOrDefault(mytype => mytype.GetInterfaces().Contains(typeof(IEventHandler<TEvent>)));
                if (handler == null)
                    return;

                Object[] args = { _serviceProvider };

                var instance = Activator.CreateInstance(handler, args);

                if (instance == null)
                    return;

                var parameter = new object[] { @event };

                var response = handler.GetMethods()
                                       .Single(method => method.GetParameters().Length == 1 &&
                                               method.GetParameters()[0].ParameterType == typeof(TEvent));
                if (response == null)
                    return;

                response.Invoke(instance, parameter);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
