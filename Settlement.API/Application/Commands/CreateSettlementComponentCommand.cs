using MediatR;
using PowerIndustryOnMicroservices.Common.RabbitMQ.Message;
using System;

namespace Settlement.API.Application.Commands
{
	public class CreateSettlementComponentCommand : ICommand, IRequest<CreateSettlementComponentResponse>
	{
		public string Name { get; set; }
		public decimal UnitPrice { get; set; }
		public Guid CorrelationId { get; set; }
	}

	public class CreateSettlementComponentResponse : CommandResponse
	{
		public Guid? SettlementComponentId { get; set; }
	}
}
