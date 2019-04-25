using Settlement.Domain.AggregateModels.SettlementComponentAggregate;
using System;

namespace Settlement.Domain.AggregateModels.SettlementPlanAggregate
{
	public class SettlementPlanComponent
	{
		public DateTime Start { get; private set; }
		public DateTime End { get; private set; }

		public Guid SettlementComponentId { get; private set; }
		public SettlementComponent SettlementComponent { get; private set; }

		public SettlementPlanComponent(DateTime start, DateTime end, SettlementComponent settlementComponent)
		{
			Start = start;
			End = end;
			SettlementComponentId = settlementComponent.Id;
			SettlementComponent = settlementComponent;
		}

		public void SetSettlementComponent(SettlementComponent settlementComponent)
		{
			if (SettlementComponentId != settlementComponent.Id)
			{
				throw new InvalidOperationException();
			}

			SettlementComponent = settlementComponent;
		}
	}
}
