using System;

namespace Invoice.Domain.AggregateModels.SettlementPlanAggreagate
{
	public class SettlementPlanComponent
	{
		public DateTime Start { get; private set; }
		public DateTime End { get; private set; }
		public Guid SettlementComponentId { get; private set; }
	}
}
