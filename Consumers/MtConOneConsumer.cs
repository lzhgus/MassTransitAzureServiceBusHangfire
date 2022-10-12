using System;
using System.Collections;
using Hangfire.Server;
using MassTransit.Scheduling;
using Microsoft.Extensions.Logging;
using MtHangfire.Cadence;

namespace Company.Consumers
{
    using System.Threading.Tasks;
    using MassTransit;
    using Contracts;

    public class MtConOneConsumer :
        IConsumer<MtConOne>
    {
        private ILogger<MtConOneConsumer> _logger;

        public MtConOneConsumer(ILogger<MtConOneConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<MtConOne> context)
        {
            _logger.LogInformation("Received the first message");

            Uri scheduler = new Uri("queue:mt-consumer-2");

            await context.ScheduleSend(scheduler, DateTime.Now.AddSeconds(30), context.Message);

            // await context.ScheduleRecurringSend(scheduler, new PollExternalSystemSchedule(),  context.Message);

        }
    }
}
