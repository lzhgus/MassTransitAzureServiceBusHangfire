namespace Company.Consumers
{
    using MassTransit;

    public class MtConOneConsumerDefinition :
        ConsumerDefinition<MtConOneConsumer>
    {
        public MtConOneConsumerDefinition()
        {
            EndpointName = "mt-consumer-1";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<MtConOneConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}
