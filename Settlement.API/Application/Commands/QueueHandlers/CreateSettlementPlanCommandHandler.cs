using PowerIndustryOnMicroservices.Common.RabbitMQ.Message;
using Settlement.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Settlement.API.Application.Commands.QueueHandlers
{
	public class CreateSettlementPlanCommandHandler : ICommandHandler<CreateSettlementPlanCommand>
	{
		private ICreateSettlementPlanService _createSettlementPlanService;

		public CreateSettlementPlanCommandHandler(ICreateSettlementPlanService createSettlementPlanService)
		{
			_createSettlementPlanService = createSettlementPlanService;
		}

		public Task HandleAsync(CreateSettlementPlanCommand command, CancellationToken cancellationToken)
		{
			return Task.FromResult(_createSettlementPlanService.CreateSettlementPlant(command.PayerId));
		}
	}
}
