using System.Collections.Generic;

namespace Settlement.Domain.AggregateModels.SettlementComponentAggregate
{
	public interface ISettlementComponentRepository
	{
		SettlementComponent Add(SettlementComponent settlementComponent);
		List<SettlementComponent> GetAll();
	}
}
