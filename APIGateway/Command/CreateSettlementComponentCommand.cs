using System;
using PowerIndustryOnMicroservices.Common.RabbitMQ.Message;

namespace APIGateway.Command
{
	public class CreateSettlementComponentCommand : ICommand
	{
		public string Name { get; }
		public decimal UnitPrice { get; }
		public Guid CorrelationId { get; set; }

		public CreateSettlementComponentCommand(string name, decimal unitPrice, Guid correlationId)
		{
			Name = name;
			UnitPrice = unitPrice;
			CorrelationId = correlationId;
		}
	}
}
