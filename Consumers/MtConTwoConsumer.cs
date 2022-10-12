using System;
using Microsoft.Extensions.Logging;

namespace Company.Consumers
{
    using System.Threading.Tasks;
    using MassTransit;
    using Contracts;

    public class MtConTwoConsumer :
        IConsumer<MtConOne>
    {
        private ILogger<MtConTwoConsumer> _logger;

        public MtConTwoConsumer(ILogger<MtConTwoConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<MtConOne> context)
        {
            _logger.LogInformation("Received Message at: {Text}", DateTime.UtcNow.ToString());
            return Task.CompletedTask;
        }
    }
}
