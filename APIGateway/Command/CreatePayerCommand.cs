using PowerIndustryOnMicroservices.Common.RabbitMQ.Message;
using System;
namespace APIGateway.Command
{
	public class CreatePayerCommand : ICommand
	{
		public CreatePayerCommand(string name, Guid correlationId)
		{
			Name = name;
			CorrelationId = correlationId;
		}

		public string Name { get; }
		public Guid CorrelationId { get; set; }
	}
}
