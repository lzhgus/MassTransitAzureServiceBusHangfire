namespace Company.Consumers
{
    using MassTransit;

    public class MtConTwoConsumerDefinition :
        ConsumerDefinition<MtConTwoConsumer>
    {
        public MtConTwoConsumerDefinition()
        {
            EndpointName = "mt-consumer-2";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<MtConTwoConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}
