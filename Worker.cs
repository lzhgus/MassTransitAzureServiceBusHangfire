using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Hangfire;

public class Worker : BackgroundService
{
    private readonly IBus _bus;

    public Worker(IBus bus)
    {
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var endpoint = await _bus.GetSendEndpoint(new Uri("queue:mt-consumer-1"));

        await endpoint.Send(new MtConOne
        {
            Value = DateTime.Now
        });
    }
}
