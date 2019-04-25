using MediatR;
using PowerIndustryOnMicroservices.Common.RabbitMQ.Message;
using System;

namespace Settlement.API.Application.Commands
{
	public class CreateSettlementPlanCommand : ICommand, IRequest<CreateSettlementPlanResponse>
	{
		public Guid PayerId { get; set; }
		public Guid CorrelationId { get; set; }

		public CreateSettlementPlanCommand(Guid payerId, Guid correlationId)
		{
			PayerId = payerId;
			CorrelationId = correlationId;
		}
	}

	public class CreateSettlementPlanResponse : CommandResponse
	{
		public Guid? SettlementPlanId { get; set; }
	}
}
