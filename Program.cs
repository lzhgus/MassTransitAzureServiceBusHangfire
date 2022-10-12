using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Company.Consumers;
using Hangfire;
using Hangfire.MemoryStorage;
using MassTransit.HangfireIntegration;
using Microsoft.Extensions.DependencyInjection;

namespace MtHangfire
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHangfire(config =>
                    {
                        config.UseMemoryStorage();
                    });

                    services.AddSingleton<IHangfireComponentResolver, HangfireComponentResolver>();
                    services.AddHangfireServer();
                    services.AddHostedService<Worker>();
                    services.AddMassTransit(x =>
                    {

                        x.AddMessageScheduler(new Uri("queue:hangfire"));
                        x.AddConsumer<MtConOneConsumer, MtConOneConsumerDefinition>();
                        x.AddConsumer<MtConTwoConsumer, MtConTwoConsumerDefinition>();

                        x.UsingAzureServiceBus((context, cfg) =>
                        {
                            cfg.Host("Endpoint=");
                            cfg.UseHangfireScheduler(context.GetRequiredService<IHangfireComponentResolver>(),
                                "hangfire");
                            cfg.ConfigureEndpoints(context);
                        });
                    });
                });
    }
}
