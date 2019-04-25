using System;
using System.Collections.Generic;
using System.Text;
using Settlement.Domain.AggregateModels.PayerAggregate;
using Settlement.Domain.AggregateModels.SettlementComponentAggregate;

namespace Settlement.Domain.AggregateModels.SettlementPlanAggregate
{
	public interface ISettlementPlanFactory
	{
		SettlementPlan CreateSettlementPlan(Guid id, IList<SettlementPlanComponent> settlementPlanComponents1, Guid payerId, Payer payer, List<SettlementComponent> settlementPlanComponents2);
	}
}
