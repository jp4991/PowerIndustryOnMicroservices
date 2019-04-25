using MediatR;
using Settlement.Domain.AggregateModels.PayerAggregate;
using Settlement.Domain.AggregateModels.SettlementComponentAggregate;
using Settlement.Domain.AggregateModels.SettlementPlanAggregate;
using Settlement.Domain.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Settlement.API.Application.Commands
{
	public class CreateSettlementPlanCommandHandler : IRequestHandler<CreateSettlementPlanCommand, CreateSettlementPlanResponse>
	{
		private readonly ICreateSettlementPlanService _createSettlementPlanService;

		public CreateSettlementPlanCommandHandler(ICreateSettlementPlanService createSettlementPlanService)
		{
			_createSettlementPlanService = createSettlementPlanService;
		}

		public async Task<CreateSettlementPlanResponse> Handle(CreateSettlementPlanCommand request,
			CancellationToken cancellationToken)
		{
			var response = new CreateSettlementPlanResponse();
			try
			{
				

				response.Success = true;
				response.SettlementPlanId = await _createSettlementPlanService.CreateSettlementPlant(request.PayerId);

				return response;
			}
			catch (Exception)
			{
				response.Success = false;
				response.ErrorDescription = "Wystąpił błąd w trakcie tworzenia planu rozliczeń";

				return response;
			}
		}
	}
}
