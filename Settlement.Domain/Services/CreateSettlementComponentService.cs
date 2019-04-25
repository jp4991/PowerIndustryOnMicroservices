using Settlement.Domain.AggregateModels.SettlementComponentAggregate;
using System;

namespace Settlement.Domain.Services
{
	public class CreateSettlementComponentService : ICreateSettlementComponentService
	{
		private readonly ISettlementComponentRepository _settlementComponentRepository;

		public CreateSettlementComponentService(ISettlementComponentRepository settlementComponentRepository)
		{
			_settlementComponentRepository = settlementComponentRepository;
		}

		public Guid CreateSettlementComponent(string name, decimal unitPrice)
		{
				var settlementComponent = new SettlementComponent(name, unitPrice);
				var component = _settlementComponentRepository.Add(settlementComponent);
			return component.Id;
		}
	}
}
