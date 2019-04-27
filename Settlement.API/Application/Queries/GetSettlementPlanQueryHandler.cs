using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Settlement.API.Dto;
using Settlement.Domain.AggregateModels.SettlementPlanAggregate;

namespace Settlement.API.Application.Queries
{
	public class GetSettlementPlanQueryHandler : IRequestHandler<GetSettlementPlanQuery, SettlementPlanDto>
	{
		private readonly ISettlementPlanRepository _settlementPlanRepository;

		public GetSettlementPlanQueryHandler(ISettlementPlanRepository settlementPlanRepository)
		{
			_settlementPlanRepository = settlementPlanRepository;
		}

		public Task<SettlementPlanDto> Handle(GetSettlementPlanQuery request, CancellationToken cancellationToken)
		{
			var domainSettlementPlan = _settlementPlanRepository.Get(request.SettlementPlanId);
			var settlementPlanComponentDtos = domainSettlementPlan
				.SettlementPlanComponents
				.Select(x => new SettlementPlanComponentDto
				{
					SettlementComponentId = x.SettlementComponentId,
					SettlementComponentName = x.SettlementComponent.Name,
					Start = x.Start,
					End = x.End
				})
				.ToList();

			var planDto = new SettlementPlanDto()
			{
				Id = domainSettlementPlan.Id,
				PayerId = domainSettlementPlan.PayerId,
				Components = settlementPlanComponentDtos
			};

			return Task.FromResult(planDto);
		}
	}
}
