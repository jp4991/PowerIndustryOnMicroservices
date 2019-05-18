using MediatR;
using PowerIndustryOnMicroservices.Common.RabbitMQ.Message;
using System;

namespace Settlement.API.Application.Commands
{
	public class CreatePayerCommand : ICommand, IRequest<CreatePayerResponse>
	{
		public string Name { get; set; }
		public decimal PriceDiscount { get; set; }
		public Guid CorrelationId { get; set; }
	}

	public class CreatePayerResponse : CommandResponse
	{
		public Guid? PayerId { get; set; }
	}
}
