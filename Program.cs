using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Company.Consumers;
using Hangfire;
using Hangfire.Azure;
using Hangfire.MemoryStorage;
using MassTransit.HangfireIntegration;
using Microsoft.Azure.Cosmos;
using Hangfire.SqlServer;
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
                    // ConfigureHangfireSqlServer(services);
                    ConfigureHangfireCosmosDb(services);
                    // ConfigureHangfireInMemory(services);
                    services.AddHostedService<Worker>();
                    services.AddMassTransit(x =>
                    {

                        x.AddMessageScheduler(new Uri("queue:scheduler"));
                        x.AddConsumer<MtConOneConsumer, MtConOneConsumerDefinition>();
                        x.AddConsumer<MtConTwoConsumer, MtConTwoConsumerDefinition>();

                        x.UsingAzureServiceBus((context, cfg) =>
                        {
                            cfg.Host(
                                "");
                            // cfg.UseHangfireScheduler(context.GetRequiredService<IHangfireComponentResolver>(),
                            //     "scheduler");
                            cfg.UseHangfireScheduler("scheduler");
                            cfg.UseMessageScheduler(new Uri("queue:scheduler"));
                            cfg.ConfigureEndpoints(context);
                        });
                    });
                });

        private static void ConfigureHangfireCosmosDb(IServiceCollection services)
        {
            var options = new CosmosDbStorageOptions()
            {
                ExpirationCheckInterval = TimeSpan.FromMinutes(2),
                CountersAggregateInterval = TimeSpan.FromMinutes(2),
                QueuePollInterval = TimeSpan.Zero
                // JobKeepAliveInterval = TimeSpan.FromMinutes(5)
            };

            var cosmoClientOptions = new CosmosClientOptions
            {
                ApplicationName = "hangfire",
                RequestTimeout = TimeSpan.FromSeconds(60),
                ConnectionMode = ConnectionMode.Direct,
                MaxRetryAttemptsOnRateLimitedRequests = 3,
                MaxRetryWaitTimeOnRateLimitedRequests = TimeSpan.FromSeconds(30)
            };

            // services.AddHangfireServer();
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseAzureCosmosDbStorage("",
                    "",
                    "SchedulerDB", "Schedules", storageOptions: options));

            JobStorage.Current = CosmosDbStorage.Create("https://", "",
                "SchedulerDB", "Schedules", cosmoClientOptions, options);
        }

        private static void ConfigureHangfireSqlServer(IServiceCollection services)
        {
            var connectionString = "";

            JobStorage.Current = new SqlServerStorage(connectionString);

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));
        }

        private static void ConfigureHangfireInMemory(IServiceCollection services)
        {
            services.AddHangfire(config =>
            {
                config.UseMemoryStorage();
            });
            services.AddHangfireServer();
            services.AddSingleton<IHangfireComponentResolver, HangfireComponentResolver>();
        }
    }
}
