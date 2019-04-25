using Invoice.Domain.SeedWork;
using System;

namespace Invoice.Domain.AggregateModels.SettlementComponentAggreagate
{
	public class SettlementComponent : IAggregateRoot
	{
		public Guid Id { get; private set; }
		public string Name { get; private set; }
		public decimal UnitPrice { get; set; }
	}
}
