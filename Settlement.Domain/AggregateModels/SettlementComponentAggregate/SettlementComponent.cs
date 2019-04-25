using Settlement.Domain.SeedWork;
using System;

namespace Settlement.Domain.AggregateModels.SettlementComponentAggregate
{
	public class SettlementComponent : IAggregateRoot
	{
		public Guid Id { get; private set; }
		public string Name { get; private set; }
		public decimal UnitPrice { get; set; }

		public SettlementComponent(string name, decimal unitPrice)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new InvalidOperationException();
			}

			if (unitPrice < 0M)
			{
				throw new InvalidOperationException();
			}

			Name = name;
			UnitPrice = unitPrice;
		}
	}
}
