using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChannelConsumer.Service;
using ChannelConsumer.Workers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChannelConsumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Consumer>();
                    services.AddHostedService<ProducerOne>();
                    services.AddHostedService<ProducerTwo>();
                    services.AddSingleton<ChannelService>();
                });
    }
}
