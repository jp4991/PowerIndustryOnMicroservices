using PowerIndustryOnMicroservices.Common.RabbitMQ.Message;
using System;
namespace APIGateway.Command
{
	public class CreatePayerCommand : ICommand
	{
		public CreatePayerCommand(string name, decimal priceDiscount, Guid correlationId)
		{
			Name = name;
			CorrelationId = correlationId;
			PriceDiscount = priceDiscount;
		}

		public string Name { get; }
		public Guid CorrelationId { get; set; }
		public decimal PriceDiscount { get; set; }
	}
}
