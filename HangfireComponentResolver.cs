using System;
using System.Collections.Generic;
using Hangfire;
using Hangfire.Common;
using Hangfire.Server;
using MassTransit.HangfireIntegration;
using Microsoft.Extensions.DependencyInjection;

namespace MtHangfire
{
    public class HangfireComponentResolver :
        IHangfireComponentResolver
    {
        readonly IServiceProvider _serviceProvider;

        public HangfireComponentResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IBackgroundJobClient BackgroundJobClient => _serviceProvider.GetService<IBackgroundJobClient>();
        public IRecurringJobManager RecurringJobManager => _serviceProvider.GetService<IRecurringJobManager>();
        public ITimeZoneResolver TimeZoneResolver => _serviceProvider.GetService<ITimeZoneResolver>();
        public IJobFilterProvider JobFilterProvider => _serviceProvider.GetService<IJobFilterProvider>();
        public IEnumerable<IBackgroundProcess> BackgroundProcesses => _serviceProvider.GetServices<IBackgroundProcess>();
        public JobStorage JobStorage => _serviceProvider.GetService<JobStorage>();
    }
}
