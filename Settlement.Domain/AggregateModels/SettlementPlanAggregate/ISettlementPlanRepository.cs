using System;

namespace Settlement.Domain.AggregateModels.SettlementPlanAggregate
{
	public interface ISettlementPlanRepository
	{
		SettlementPlan Add(SettlementPlan settlementPlan);
		SettlementPlan Get(Guid id);
	}
}
