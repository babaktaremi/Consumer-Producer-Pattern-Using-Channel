using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChannelConsumer.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChannelConsumer
{
    public class Consumer : BackgroundService
    {
        private readonly ILogger<Consumer> _logger;
        private readonly IServiceProvider _serviceProvider;
        public Consumer(ILogger<Consumer> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var scope = _serviceProvider.CreateScope();

                var service = scope.ServiceProvider.GetService<ChannelService>();

             
                    await foreach (var item in service.ReturnValue(stoppingToken))
                    {
                        _logger.LogInformation("Worker Consuming item: {item}", item);
                        await Task.Delay(10_000, stoppingToken);
                    }
                

               
            }
        }
    }
}
