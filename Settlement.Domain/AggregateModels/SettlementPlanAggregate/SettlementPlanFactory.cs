using System;
using System.Collections.Generic;
using System.Linq;
using Settlement.Domain.AggregateModels.PayerAggregate;
using Settlement.Domain.AggregateModels.SettlementComponentAggregate;

namespace Settlement.Domain.AggregateModels.SettlementPlanAggregate
{
	public class SettlementPlanFactory : ISettlementPlanFactory
	{
		public SettlementPlan CreateSettlementPlan(Guid id,
			IList<SettlementPlanComponent> settlementPlanComponents,
			Guid payerId,
			Payer payer,
			List<SettlementComponent> settlementComponents)
		{
			foreach (var planComponent in settlementPlanComponents)
			{
				var settlementComponent = settlementComponents.Single(x => x.Id == planComponent.SettlementComponentId);
				planComponent.SetSettlementComponent(settlementComponent);
			}


			return new SettlementPlan(id, payerId, payer, settlementPlanComponents);
		}
	}
}
