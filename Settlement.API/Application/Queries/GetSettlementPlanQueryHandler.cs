using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Settlement.Domain.AggregateModels.SettlementPlanAggregate;

namespace Settlement.API.Application.Queries
{
	public class GetSettlementPlanQueryHandler : IRequestHandler<GetSettlementPlanQuery, SettlementPlan>
	{
		private readonly ISettlementPlanRepository _settlementPlanRepository;

		public GetSettlementPlanQueryHandler(ISettlementPlanRepository settlementPlanRepository)
		{
			_settlementPlanRepository = settlementPlanRepository;
		}

		public Task<SettlementPlan> Handle(GetSettlementPlanQuery request, CancellationToken cancellationToken)
		{
			return Task.FromResult(_settlementPlanRepository.Get(request.SettlementPlanId));
		}
	}
}
