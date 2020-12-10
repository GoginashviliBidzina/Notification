using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Notification.Application.Infrastructure;
using Notification.Application.Notifications;
using Notification.Application.Shared.Helpers;
using Notification.Infrastructure.Configuration;

namespace Notification
{
    public class Worker : BackgroundService
    {
        private static IQueueClient queueClient;
        private readonly ILogger<Worker> _logger;
        private readonly EventDispatcher _dispatcher;
        private readonly AzureServiceBusConfig _configuration;

        public Worker(ILogger<Worker> logger,
                      EventDispatcher dispatcher,
                      IOptions<AzureServiceBusConfig> configuration)
        {
            _logger = logger;
            _dispatcher = dispatcher;
            _configuration = configuration.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ProcessMessagesAsync();
        }

        async Task ProcessMessagesAsync()
        {
            await using (ServiceBusClient client = new ServiceBusClient(_configuration.ConnectionString))
            {
                ServiceBusProcessor processor = client.CreateProcessor(_configuration.QueueName, new ServiceBusProcessorOptions());

                processor.ProcessMessageAsync += MessageHandler;
                processor.ProcessErrorAsync += ErrorHandler;

                await processor.StartProcessingAsync();

                Console.WriteLine("Wait for a minute and then press any key to end the processing");
                Console.ReadKey();

                Console.WriteLine("\nStopping the receiver...");
                await processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            }
        }

        async Task MessageHandler(ProcessMessageEventArgs args)
        {
            var eventType = args.Message.ApplicationProperties["eventType"].ToString().ToLower();
            if (eventType == nameof(DefaultEventModel).ToLower())
            {
                var message = args.DeserializeMessage<DefaultEventModel>();
                await _dispatcher.Dispatch(message);
            }
            else if (eventType == nameof(ProductAddedEvent).ToLower())
            {
                var message = args.DeserializeMessage<ProductAddedEvent>();
                await _dispatcher.Dispatch(message);
            }

            await args.CompleteMessageAsync(args.Message);
        }

        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());

            return Task.CompletedTask;
        }
    }
}
