using System;
using MassTransit.Initializers.Variables;

namespace Contracts
{
    public record MtConOne
    {
        public DateTime Value { get; init; }
    }
}
