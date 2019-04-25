using Invoice.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Invoice.Domain.AggregateModels.SettlementPlanAggreagate
{
	public class SettlementPlan : IAggregateRoot
	{
		public IList<SettlementPlanComponent> SettlementPlanComponents { get; private set; }
		public Guid Id { get; private set; }
		public Guid PayerId { get; private set; }
	}
}
