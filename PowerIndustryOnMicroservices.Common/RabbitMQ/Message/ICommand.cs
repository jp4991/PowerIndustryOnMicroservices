using System;

namespace PowerIndustryOnMicroservices.Common.RabbitMQ.Message
{
	public interface ICommand
	{
		Guid CorrelationId { get; set; }
	}
}
