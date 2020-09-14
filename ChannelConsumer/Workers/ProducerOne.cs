using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChannelConsumer.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChannelConsumer.Workers
{
   public class ProducerOne:BackgroundService
    {
        private readonly ILogger<ProducerOne> _logger;
        private readonly IServiceProvider _serviceProvider;
        public ProducerOne(ILogger<ProducerOne> logger, IServiceProvider serviceProvider)
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

                for (int i = 0; i < 100; i++)
                {
                    await service.AddToChannelAsync(i.ToString(), stoppingToken);
                    _logger.LogInformation("First Worker Producing item: {i}", i);
                    await Task.Delay(2000,stoppingToken);
                }

            }
        }
    }
}
