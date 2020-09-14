using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChannelConsumer.Service;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChannelConsumer.Workers
{
   public class ProducerTwo:BackgroundService
   {
       private readonly ChannelService _channelService;
       private readonly ILogger<ProducerTwo> _logger;
        public ProducerTwo(ChannelService channelService, ILogger<ProducerTwo> logger)
        {
            _channelService = channelService;
            _logger = logger;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            for (int i = 100; i < 200; i++)
            {
                await _channelService.AddToChannelAsync(i.ToString(), stoppingToken);
                _logger.LogInformation("Second Worker Producing item: {i}", i);
                await Task.Delay(100,stoppingToken);
            }
        }
    }
}
