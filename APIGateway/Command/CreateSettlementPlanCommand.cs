using PowerIndustryOnMicroservices.Common.RabbitMQ.Message;
using System;

namespace APIGateway.Command
{
	public class CreateSettlementPlanCommand : ICommand
	{
		public CreateSettlementPlanCommand(Guid payerId, Guid correlationId)
		{
			PayerId = payerId;
			CorrelationId = correlationId;
		}

		public Guid PayerId { get; set; }
		public Guid CorrelationId { get; set; }
	}
}
