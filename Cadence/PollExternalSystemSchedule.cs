using System;
using MassTransit.Scheduling;

namespace MtHangfire.Cadence
{
public class PollExternalSystemSchedule : DefaultRecurringSchedule
{
    public PollExternalSystemSchedule()
    {
        CronExpression = CronExpressions.EveryMinute;
    }
}

public class PollExternalSystem {}

}
